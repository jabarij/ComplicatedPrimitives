using ComplicatedPrimitives.TestAbstractions;
using System;
using Xunit;
using AutoFixture;
using FluentAssertions;

namespace ComplicatedPrimitives.Tests;

partial class DirectedLimitTests
{
    public class Intersects : DirectedLimitTests
    {
        public Intersects(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void SameSide_ShouldReturnTrue()
        {
            // arrange
            var sut = Create<int>(side: LimitSide.Left);
            var other = Create<int>(side: sut.Side);

            // act
            bool result1 = sut.Intersects(other);
            bool result2 = other.Intersects(sut);

            // assert
            result1.Should().BeTrue(because: "{0} and {1} are of same side so must have intersection", sut, other);
            result2.Should().BeTrue(because: "{0} and {1} are of same side so must have intersection", sut, other);
        }

        [Theory]
        [InlineData(LimitType.Open, 1, 1, LimitType.Open, false)]
        [InlineData(LimitType.Open, 1, 1, LimitType.Closed, false)]
        [InlineData(LimitType.Closed, 1, 1, LimitType.Open, false)]
        [InlineData(LimitType.Closed, 1, 1, LimitType.Closed, true)]
        [InlineData(LimitType.Open, 1, 2, LimitType.Open, true)]
        [InlineData(LimitType.Open, 2, 1, LimitType.Open, false)]
        public void ShouldReturnExpectedResult(LimitType leftType, int leftValue, int rightValue, LimitType rightType, bool expected)
        {
            // arrange
            var left = Create<int>(value: leftValue, type: leftType, side: LimitSide.Left);
            var right = Create<int>(value: rightValue, type: rightType, side: LimitSide.Right);

            // act
            bool result = right.Intersects(left);

            // assert
            result.Should().Be(expected, because: "{0} {2} a proper superset of {1}", right, left, expected ? "is" : "is not");
        }

        private DirectedLimit<T> Create<T>(
            T? value = null,
            LimitType? type = null,
            LimitSide? side = null)
            where T : struct, IComparable<T> =>
            new(
                value: value ?? Fixture.Create<T>(),
                type: type ?? Fixture.Create<LimitType>(),
                side: side ?? Fixture.Create<LimitSide>());
    }
}