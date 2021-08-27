using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
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
            [InlineData(2, 1, LimitSide.Left, false)]
            [InlineData(1, 2, LimitSide.Left, true)]
            [InlineData(1, 1, LimitSide.Right, false)]
            [InlineData(2, 1, LimitSide.Right, true)]
            [InlineData(1, 2, LimitSide.Right, false)]
            public void PointValueChanging_CeterisParibus_ShouldReturnExpectedResult(int sutValue, int otherValue, LimitSide side, bool expected)
            {
                // arrange
                var sut = Create<int>(value: sutValue, side: side);
                var other = sut.With(value: otherValue);

                // act
                bool result = sut.IsProperSupersetOf(other);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper superset of {1}", sut, other, expected ? "is" : "is not");
            }

            [Theory]
            [InlineData(LimitPointType.Open, LimitPointType.Open, false)]
            [InlineData(LimitPointType.Closed, LimitPointType.Open, true)]
            [InlineData(LimitPointType.Open, LimitPointType.Closed, false)]
            [InlineData(LimitPointType.Closed, LimitPointType.Closed, false)]
            public void PointTypeChanging_CeterisParibus_ShouldReturnExpectedResult(LimitPointType sutType, LimitPointType otherType, bool expected)
            {
                // arrange
                var sut = Create<int>(type: sutType);
                var other = sut.With(type: otherType);

                // act
                bool result = sut.IsProperSupersetOf(other);

                // assert
                result.Should().Be(expected, because: "{0} {2} a proper superset of {1}", sut, other, expected ? "is" : "is not");
            }
        }
    }
}
