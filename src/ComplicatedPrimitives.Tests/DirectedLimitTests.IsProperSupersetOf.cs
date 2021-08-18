using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class DirectedLimitTests
    {
        public class IsProperSupersetOf : DirectedLimitTests
        {
            public IsProperSupersetOf(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DifferentSide_ShouldReturnFalse()
            {
                // arrange
                var sut = Create<int>(side: LimitSide.Left);
                var other = Create<int>(side: LimitSide.Right);

                // act
                bool result1 = sut.IsProperSupersetOf(other);
                bool result2 = other.IsProperSupersetOf(sut);

                // assert
                result1.Should().BeFalse(because: "{0} and {1} are of different side so neither is proper superset of the other one", sut, other);
                result2.Should().BeFalse(because: "{0} and {1} are of different side so neither is proper superset of the other one", sut, other);
            }

            [Theory]
            [InlineData(1, 1, LimitSide.Left, false)]
            [InlineData(1, 2, LimitSide.Left, false)]
            [InlineData(2, 1, LimitSide.Left, true)]
            [InlineData(1, 1, LimitSide.Right, false)]
            [InlineData(1, 2, LimitSide.Right, true)]
            [InlineData(2, 1, LimitSide.Right, false)]
            public void ValueDependent_ShouldReturnExpectedResult(int subsetValue, int supersetValue, LimitSide side, bool expected)
            {
                // arrange
                var subset = Create<int>(value: subsetValue, side: side);
                var superset = subset.With(value: supersetValue);

                // act
                bool result = superset.IsProperSupersetOf(subset);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper superset of {1}", superset, subset, expected ? "is" : "is not");
            }

            [Theory]
            [InlineData(LimitPointType.Open, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Open, LimitPointType.Closed, true)]
            [InlineData(LimitPointType.Closed, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Closed, LimitPointType.Closed, false)]
            public void TypeDependent_ShouldReturnExpectedResult(LimitPointType subsetType, LimitPointType supersetType, bool expected)
            {
                // arrange
                var subset = Create<int>(type: subsetType);
                var superset = subset.With(type: supersetType);

                // act
                bool result = superset.IsProperSupersetOf(subset);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper superset of {1}", superset, subset, expected ? "is" : "is not");
            }

            private DirectedLimit<T> Create<T>(
                T? value = null,
                LimitPointType? type = null,
                LimitSide? side = null)
                where T : struct, IComparable<T> =>
                new DirectedLimit<T>(
                    value: value ?? Fixture.Create<T>(),
                    type: type ?? Fixture.Create<LimitPointType>(),
                    side: side ?? Fixture.Create<LimitSide>());
        }
    }
}
