using AutoFixture;
using AutoFixture.Kernel;
using ComplicatedPrimitives.TestAbstractions.Customizations.Generic;
using DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ComplicatedPrimitives.TestAbstractions;

public static class FixtureExtensions
{
    private static readonly Random _randomGenerator = new(Guid.NewGuid().ToByteArray()[0]);
    private static readonly decimal _decimalEpsilon = new(1, 0, 0, false, 28);

    public static int CreateBetween(this IFixture fixture, int min, int max) =>
        _randomGenerator.Next(min + 1, max);
    public static int CreateInRange(this IFixture fixture, int min, int max) =>
        _randomGenerator.Next(min, max);
    public static int CreateGreaterThan(this IFixture fixture, int min) =>
        CreateBetween(fixture, min, int.MaxValue);
    public static int CreateGreaterThanOrEqual(this IFixture fixture, int min) =>
        CreateInRange(fixture, min, int.MaxValue);
    public static int CreateLowerThan(this IFixture fixture, int max) =>
        CreateBetween(fixture, int.MinValue, max);
    public static int CreateLowerThanOrEqual(this IFixture fixture, int max) =>
        CreateInRange(fixture, int.MinValue, max);
    public static int CreateOtherThan(this IFixture fixture, int arg) =>
        fixture.Create<bool>()
            ? CreateGreaterThan(fixture, arg)
            : CreateLowerThan(fixture, arg);

    public static decimal CreateInRange(this IFixture fixture, decimal min, decimal max)
    {
        decimal seed = new decimal(_randomGenerator.Next(), _randomGenerator.Next(), _randomGenerator.Next(), false, 28);
        if (System.Math.Sign(min) == System.Math.Sign(max) || min == 0 || max == 0)
            return decimal.Remainder(seed, max - min) + min;

        bool getLeftNegativeRange = (double)min + _randomGenerator.NextDouble() * ((double)max - (double)min) < 0;
        return
            getLeftNegativeRange
                ? decimal.Remainder(seed, -min) + min
                : decimal.Remainder(seed, max);
    }
    public static decimal CreateGreaterThanOrEqual(this IFixture fixture, decimal min)
    {
        if (min == decimal.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate decimal number greater than decimal.MaxValue.");

        return CreateInRange(fixture, min, decimal.MaxValue);
    }
    public static decimal CreateGreaterThan(this IFixture fixture, decimal min)
    {
        if (min == decimal.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate decimal number greater than decimal.MaxValue.");

        decimal result = CreateInRange(fixture, min, decimal.MaxValue);
        return
            result == min
                ? result + _decimalEpsilon
                : result;
    }
    public static decimal CreateLowerThan(this IFixture fixture, decimal max)
    {
        if (max == decimal.MinValue)
            throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate decimal number lower than decimal.MinValue.");

        decimal result = CreateInRange(fixture, decimal.MinValue, max);
        return
            result == max
                ? result - _decimalEpsilon
                : result;
    }

    public static double CreateInRange(this IFixture fixture, double min, double max)
    {
        if (min >= max)
            throw new ArgumentOutOfRangeException(nameof(min), "Min value must be lower than max value.");

        double seed = _randomGenerator.NextDouble();
        double exclusiveMin = min > double.MinValue ? min - double.Epsilon : min;
        double result = exclusiveMin + seed * (max - exclusiveMin);
        return System.Math.Max(result, min);
    }
    public static double CreateGreaterThanOrEqual(this IFixture fixture, double min)
    {
        if (min == double.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate double number greater than double.MaxValue.");

        return CreateInRange(fixture, min, double.MaxValue);
    }
    public static double CreateGreaterThan(this IFixture fixture, double min)
    {
        if (min == double.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(min), "Cannot generate double number greater than double.MaxValue.");

        return CreateInRange(fixture, min + double.Epsilon, double.MaxValue);
    }
    public static double CreateLowerThan(this IFixture fixture, double max)
    {
        if (max == double.MinValue)
            throw new ArgumentOutOfRangeException(nameof(max), "Cannot generate double number lower than double.MinValue.");

        return CreateInRange(fixture, double.MinValue, max);
    }

    public static TValue CreateLeftSet<TValue>(this IFixture fixture, IEnumerable<TValue> values)
    {
        var valuesList = values.ToList();
        return valuesList[_randomGenerator.Next(0, valuesList.Count)];
    }

    public static TEnum CreateEnum<TEnum>(this IFixture fixture, params TEnum[] excluded)
    {
        var enumType =
            typeof(TEnum).IsNullable(out var underlyingEnumType)
                ? underlyingEnumType
                : typeof(TEnum);
        if (!enumType.IsEnum)
            throw new NotImplementedException($"Type {enumType.FullName} is an enum type.");

        var enumValues = Enum.GetValues(enumType).Cast<TEnum>().Except(excluded);
        return CreateFromSet(fixture, enumValues);
    }
    public static TValue CreateFromSet<TValue>(this IFixture fixture, IEnumerable<TValue> values)
    {
        var valuesList = values.ToList();
        int index = _randomGenerator.Next(0, valuesList.Count);
        return valuesList[index];
    }

    public static void CustomizeAsOpenGeneric<T>(this IFixture fixture, Expression<Func<GenericParameterMock, ISpecimenContext, T>> factoryExpression) =>
        fixture.Customize(new OpenGenericsBinderCustomization<T>(factoryExpression));

    public static void CustomizeAsOpenGeneric(this IFixture fixture, Type serviceType, Type factoryType) =>
        fixture.Customize(new OpenGenericsBinderCustomization(serviceType, factoryType));
}