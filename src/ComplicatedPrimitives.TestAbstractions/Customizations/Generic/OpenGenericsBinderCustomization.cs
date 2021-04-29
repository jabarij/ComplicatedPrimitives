using AutoFixture;
using AutoFixture.Kernel;
using DotNetExtensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ComplicatedPrimitives.TestAbstractions.Customizations.Generic
{
    internal class OpenGenericsBinderCustomization : ICustomization
    {
        private readonly Type _serviceType;
        private readonly Type _factoryType;

        public OpenGenericsBinderCustomization(Type serviceType, Type factoryType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));
            if (factoryType == null)
                throw new ArgumentNullException(nameof(factoryType));
            if (!serviceType.IsGenericTypeDefinition)
                throw new ArgumentException("Service type must be generic type definition.", nameof(serviceType));
            if (!factoryType.IsGenericTypeDefinition)
                throw new ArgumentException("Factory type must be generic type definition.", nameof(factoryType));
            if (factoryType.GenericTypeArguments.Length != serviceType.GenericTypeArguments.Length)
                throw new ArgumentException("Factory type must have same list of generic arguments as service type.", nameof(factoryType));

            var factoryMethods =
                from method in factoryType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                where !method.IsGenericMethod
                    && method.ReturnType.IsGenericType
                    && method.ReturnType.GetGenericTypeDefinition() == serviceType
                let parameters = method.GetParameters()
                where parameters.Length == 1
                    && typeof(ISpecimenContext).IsAssignableFrom(parameters[0].ParameterType)
                select method;

            if (!factoryMethods.Any())
                throw new ArgumentException("Factory method is missing in factory type.", nameof(factoryType));
            if (factoryMethods.HasMany())
                throw new ArgumentException("Ambiguous factory methods in factory type.", nameof(factoryType));

            _serviceType = serviceType;
            _factoryType = factoryType;
        }

        public void Customize(IFixture fixture) =>
            fixture.Customizations.Add(new OpenGenericsSpecimenBuilder(_serviceType, _factoryType));

        class OpenGenericsSpecimenBuilder : ISpecimenBuilder
        {
            private readonly Type _serviceType;
            private readonly Type _factoryType;

            private readonly static ConcurrentDictionary<Type, Delegate> _factoryMethods = new ConcurrentDictionary<Type, Delegate>();

            public OpenGenericsSpecimenBuilder(Type serviceType, Type factoryType)
            {
                _serviceType = serviceType;
                _factoryType = factoryType;
            }

            public object Create(object request, ISpecimenContext context)
            {
                if (request is Type typeRequest
                    && typeRequest.IsGenericType
                    && typeRequest.GetGenericTypeDefinition() == _serviceType)
                {
                    var factoryMethod = _factoryMethods.GetOrAdd(typeRequest, CreateFactoryMethod, context);
                    return factoryMethod.DynamicInvoke(new object[] { context });
                }

                return new NoSpecimen();
            }

            private Delegate CreateFactoryMethod(Type typeRequest, ISpecimenContext context)
            {
                var specificFactoryType = _factoryType.MakeGenericType(typeRequest.GetGenericArguments());
                var factoryMethods =
                    from method in specificFactoryType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    where !method.IsGenericMethod
                        && method.ReturnType == typeRequest
                    let parameters = method.GetParameters()
                    where parameters.Length == 1
                        && typeof(ISpecimenContext).IsAssignableFrom(parameters[0].ParameterType)
                    select method;
                var factoryMethod = factoryMethods.Single();
                var factory = context.Resolve(specificFactoryType);
                var delegateType = typeof(Func<,>).MakeGenericType(typeof(ISpecimenContext), typeRequest);
                return factoryMethod.CreateDelegate(delegateType, factory);
            }
        }
    }

    internal class OpenGenericsBinderCustomization<T> : ICustomization
    {
        private readonly Expression<Func<GenericParameterMock, ISpecimenContext, T>> _factoryExpression;

        public OpenGenericsBinderCustomization(Expression<Func<GenericParameterMock, ISpecimenContext, T>> factoryExpression)
        {
            if (!typeof(T).IsGenericType)
                throw new ArgumentException("must be generic", nameof(T));
            _factoryExpression = factoryExpression ?? throw new ArgumentNullException(nameof(factoryExpression));
        }

        public void Customize(IFixture fixture)
        {
            var body = new OpenGenericsBinderExpressionVisitor().Visit(_factoryExpression.Body);
            var lambda = Expression.Lambda<Func<ISpecimenContext, T>>(body, OpenGenericsBinderExpressionVisitor.SpecimenContextParameterExpression);
            fixture.Customizations.Add(new OpenGenericsSpecimenBuilder(lambda.Compile()));
        }

        class OpenGenericsSpecimenBuilder : ISpecimenBuilder
        {
            private readonly Func<ISpecimenContext, T> _factory;

            public OpenGenericsSpecimenBuilder(Func<ISpecimenContext, T> factory)
            {
                _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            }

            public object Create(object request, ISpecimenContext context)
            {
                if (request is Type typeRequest
                    && typeRequest.IsGenericType
                    && typeRequest.GetGenericTypeDefinition() == typeof(T).GetGenericTypeDefinition())
                    return _factory(context);

                return new NoSpecimen();
            }
        }
    }
}
