using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using System;
using System.Linq;

namespace ComplicatedPrimitives.Tests
{
    public static class FixtureExtensions
    {
        public static Range<T> CreateRange<T>(this IFixture fixture, LimitPointType? leftType = null, LimitPointType? rightType = null)
            where T : IComparable<T>
        {
            var limits = fixture.CreateMany<T>(2).OrderBy(e => e).ToList();
            return new Range<T>(
                left: limits[0],
                right: limits[1],
                leftLimit: leftType ?? fixture.Create<LimitPointType>(),
                rightLimit: rightType ?? fixture.Create<LimitPointType>());
        }

        public static DirectedLimit<int> CreateGreaterThan(this IFixture fixture, DirectedLimit<int> min) =>
            min.Translate(e => e + fixture.CreateBetween(0, int.MaxValue - e));

        public static DirectedLimit<int> CreateLowerThan(this IFixture fixture, DirectedLimit<int> max) =>
            max.Translate(e => e - fixture.CreateBetween(0, int.MaxValue - e));

        public static void CustomizeLimitPoint(this IFixture fixture)
        {
            fixture.CustomizeAsOpenGeneric(typeof(LimitPoint<>), typeof(LimitPointFactory<>));
        }

        public static void CustomizeDirectedLimit(this IFixture fixture)
        {
            fixture.CustomizeAsOpenGeneric(typeof(DirectedLimit<>), typeof(DirectedLimitFactory<>));
        }

        public static void CustomizeRange(this IFixture fixture)
        {
            fixture.CustomizeAsOpenGeneric(typeof(Range<>), typeof(RangeFactory<>));
        }

        public static void CustomizeCaseInsensitiveString(this IFixture fixture)
        {
            fixture.Customize<CaseInsensitiveString>(e => e.FromFactory(() => new CaseInsensitiveString(fixture.Create<string>())));
        }
    }
}
