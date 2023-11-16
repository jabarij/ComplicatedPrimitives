using AutoFixture.Kernel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace ComplicatedPrimitives.TestAbstractions.Customizations.Generic;

class OpenGenericsBinderExpressionVisitor : ExpressionVisitor
{
    public static readonly ParameterExpression SpecimenContextParameterExpression =
        Expression.Parameter(typeof(ISpecimenContext), "ctx");

    private static readonly MethodInfo ISpecimenContext_Resolve =
        typeof(ISpecimenContext)
            .GetMethod(nameof(ISpecimenContext.Resolve), BindingFlags.Public | BindingFlags.Instance)!;

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (TryGetExpression_ResolveExact(node, out var resolveExactExpression))
            return resolveExactExpression;

        if (TryGetExpression_Use(node, out var useExpression))
            return useExpression;

        return base.VisitMethodCall(node);
    }

    private static readonly MethodInfo ParameterMock_ResolveExact =
        typeof(GenericParameterMock)
            .GetMethod(nameof(GenericParameterMock.ResolveExact), BindingFlags.Public | BindingFlags.Instance)!;

    private static bool TryGetExpression_ResolveExact(MethodCallExpression node,
        [NotNullWhen(true)] out Expression? expression)
    {
        if (!node.Method.IsGenericMethod
            || node.Method.GetGenericMethodDefinition() != ParameterMock_ResolveExact)
        {
            expression = null;
            return false;
        }

        var argumentType = node.Method.GetGenericArguments()[0];
        var resolve = Expression
            .Call(SpecimenContextParameterExpression, ISpecimenContext_Resolve, Expression.Constant(argumentType));
        expression = Expression.Convert(resolve, argumentType);
        return true;
    }

    private static readonly MethodInfo ParameterMock_UseValue =
        typeof(GenericParameterMock)
            .GetMethod(nameof(GenericParameterMock.UseValue), BindingFlags.Public | BindingFlags.Instance)!;

    private static bool TryGetExpression_Use(MethodCallExpression node, [NotNullWhen(true)] out Expression? expression)
    {
        if (!node.Method.IsGenericMethod
            || node.Method.GetGenericMethodDefinition() != ParameterMock_UseValue)
        {
            expression = null;
            return false;
        }

        expression = Expression.Constant(node.Arguments[0]);
        return true;
    }
}