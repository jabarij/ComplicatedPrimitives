using ComplicatedPrimitives.TestAbstractions;
using System;
using Xunit;
using AutoFixture;
using FluentAssertions;

namespace ComplicatedPrimitives.Tests
{
    partial class DirectedLimitTests
    {
        public class IsSubsetOf : DirectedLimitTests
        {
            public IsSubsetOf(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DifferentSide_ShouldReturnFalse()
            {
                // arrange
                var sut = Create<int>(side: LimitSide.Left);
                var other = Create<int>(side: LimitSide.Right);

                // act
                bool result1 = sut.IsSubsetOf(other);
                bool result2 = other.IsSubsetOf(sut);

                // assert
                result1.Should().BeFalse(because: "{0} and {1} are of different side so neither is subset of the other one", sut, other);
                result2.Should().BeFalse(because: "{0} and {1} are of different side so neither is subset of the other one", sut, other);
            }

            [Theory]
            [InlineData(1, 1, LimitSide.Left, true)]
            [InlineData(2, 1, LimitSide.Left, true)]
            [InlineData(1, 2, LimitSide.Left, false)]
            [InlineData(1, 1, LimitSide.Right, true)]
            [InlineData(2, 1, LimitSide.Right, false)]
            [InlineData(1, 2, LimitSide.Right, true)]
            public void PointValueChanging_CeterisParibus_ShouldReturnExpectedResult(int sutValue, int otherValue, LimitSide side, bool expected)
            {
                // arrange
                var sut = Create<int>(value: sutValue, side: side);
                var other = sut.With(value: otherValue);

                // act
                bool result = sut.IsSubsetOf(other);

                // assert
                result.Should().Be(expected, because: "{0} {2} a subset of {1}", sut, other, expected ? "is" : "is not");
            }

            [Theory]
            [InlineData(LimitPointType.Open, LimitPointType.Open, true)]
            [InlineData(LimitPointType.Closed, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Open, LimitPointType.Closed, true)]
            [InlineData(LimitPointType.Closed, LimitPointType.Closed, true)]
            public void PointTypeChanging_CeterisParibus_ShouldReturnExpectedResult(LimitPointType sutType, LimitPointType otherType, bool expected)
            {
                // arrange
                var sut = Create<int>(type: sutType);
                var other = sut.With(type: otherType);

                // act
                bool result = sut.IsSubsetOf(other);

                // assert
                result.Should().Be(expected, because: "{0} {2} a subset of {1}", sut, other, expected ? "is" : "is not");
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
