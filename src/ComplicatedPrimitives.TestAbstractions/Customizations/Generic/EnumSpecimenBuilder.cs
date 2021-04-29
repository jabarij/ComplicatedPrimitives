using AutoFixture.Kernel;
using DotNetExtensions;
using System;

namespace ComplicatedPrimitives.TestAbstractions.Customizations.Generic
{
    public class EnumSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly Random _randomGenerator = new Random(Guid.NewGuid().ToByteArray()[0]);

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type typeRequest)
            {
                var underlyingType =
                    typeRequest.IsNullable(out var underlyingEnumType)
                    ? underlyingEnumType
                    : typeRequest;
                if (underlyingType.IsEnum)
                {
                    var enumValues = Enum.GetValues(underlyingType);
                    int index = _randomGenerator.Next(0, enumValues.Length);
                    return enumValues.GetValue(index);
                }
            }

            return new NoSpecimen();
        }
    }
}
