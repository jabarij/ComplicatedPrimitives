using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class DirectedLimitTests
    {
        public class IsProperSubsetOf : DirectedLimitTests
        {
            public IsProperSubsetOf(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DifferentSide_ShouldReturnFalse()
            {
                // arrange
                var sut = Create<int>(side: LimitSide.Left);
                var other = Create<int>(side: LimitSide.Right);

                // act
                bool result1 = sut.IsProperSubsetOf(other);
                bool result2 = other.IsProperSubsetOf(sut);

                // assert
                result1.Should().BeFalse(because: "{0} and {1} are of different side so neither is proper subset of the other one", sut, other);
                result2.Should().BeFalse(because: "{0} and {1} are of different side so neither is proper subset of the other one", sut, other);
            }

            [Theory]
            [InlineData(1, 1, LimitSide.Left, false)]
            [InlineData(1, 2, LimitSide.Left, true)]
            [InlineData(2, 1, LimitSide.Left, false)]
            [InlineData(1, 1, LimitSide.Right, false)]
            [InlineData(1, 2, LimitSide.Right, false)]
            [InlineData(2, 1, LimitSide.Right, true)]
            public void ValueDependent_ShouldReturnExpectedResult(int supersetValue, int subsetValue, LimitSide side, bool expected)
            {
                // arrange
                var superset = Create<int>(value: supersetValue, side: side);
                var subset = superset.With(value: subsetValue);

                // act
                bool result = subset.IsProperSubsetOf(superset);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper subset of {1}", subset, superset, expected ? "is" : "is not");
            }

            [Theory]
            [InlineData(LimitType.Open, LimitType.Open, false)]
            [InlineData(LimitType.Open, LimitType.Closed, false)]
            [InlineData(LimitType.Closed, LimitType.Open, true)]
            [InlineData(LimitType.Closed, LimitType.Closed, false)]
            public void TypeDependent_ShouldReturnExpectedResult(LimitType supersetType, LimitType subsetType, bool expected)
            {
                // arrange
                var superset = Create<int>(type: supersetType);
                var subset = superset.With(type: subsetType);

                // act
                bool result = subset.IsProperSubsetOf(superset);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper subset of {1}", subset, superset, expected ? "is" : "is not");
            }

            private DirectedLimit<T> Create<T>(
                T? value = null,
                LimitType? type = null,
                LimitSide? side = null)
                where T : struct, IComparable<T> =>
                new DirectedLimit<T>(
                    value: value ?? Fixture.Create<T>(),
                    type: type ?? Fixture.Create<LimitType>(),
                    side: side ?? Fixture.Create<LimitSide>());
        }
    }
}
