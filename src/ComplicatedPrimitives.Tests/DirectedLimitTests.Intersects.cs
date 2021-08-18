using ComplicatedPrimitives.TestAbstractions;
using System;
using Xunit;
using AutoFixture;
using FluentAssertions;

namespace ComplicatedPrimitives.Tests
{
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
            [InlineData(LimitPointType.Open, 1, 1, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Open, 1, 1, LimitPointType.Closed, false)]
            [InlineData(LimitPointType.Closed, 1, 1, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Closed, 1, 1, LimitPointType.Closed, true)]
            [InlineData(LimitPointType.Open, 1, 2, LimitPointType.Open, true)]
            [InlineData(LimitPointType.Open, 2, 1, LimitPointType.Open, false)]
            public void ShouldReturnExpectedResult(LimitPointType leftType, int leftValue, int rightValue, LimitPointType rightType, bool expected)
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
