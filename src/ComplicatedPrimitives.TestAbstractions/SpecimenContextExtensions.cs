using AutoFixture.Kernel;
using DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplicatedPrimitives.TestAbstractions
{
    public static  class SpecimenContextExtensions
    {
        private static readonly Random _randomGenerator = new Random(Guid.NewGuid().ToByteArray()[0]);

        public static TEnum CreateEnum<TEnum>(this ISpecimenContext context, params TEnum[] excluded)
        {
            var enumType =
                typeof(TEnum).IsNullable(out var underlyingEnumType)
                ? underlyingEnumType
                : typeof(TEnum);
            if (!enumType.IsEnum)
                throw new NotImplementedException($"Type {enumType.FullName} is an enum type.");

            var enumValues = Enum.GetValues(enumType).Cast<TEnum>().Except(excluded);
            return CreateFromSet(context, enumValues);
        }

        public static TValue CreateFromSet<TValue>(this ISpecimenContext context, IEnumerable<TValue> values)
        {
            var valuesList = values.ToList();
            int index = _randomGenerator.Next(0, valuesList.Count);
            return valuesList[index];
        }
    }
}
