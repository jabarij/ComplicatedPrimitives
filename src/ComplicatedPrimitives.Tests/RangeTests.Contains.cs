using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests;

partial class RangeTests
{
    public class Contains : RangeTests
    {
        public Contains(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LimitType.Open, false)]
        [InlineData(LimitType.Closed, true)]
        public void LeftLimitValue_ShouldReturnExpectedResult(LimitType leftLimit, bool expectedResult)
        {
            // arrange
            var sut = Fixture.CreateRange<decimal>(leftType: leftLimit);

            // act
            bool result = sut.Contains(sut.Left.Value);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(LimitType.Open, false)]
        [InlineData(LimitType.Closed, true)]
        public void RightLimitValue_ShouldReturnExpectedResult(LimitType rightLimit, bool expected)
        {
            // arrange
            var sut = Fixture.CreateRange<decimal>(rightType: rightLimit);

            // act
            bool result = sut.Contains(sut.Right.Value);

            // assert
            result.Should().Be(expected, because: "{0} is the right limit value of {1} and {2} part of {3} limit", sut.Right.Value, sut, expected ? "is" : "is not", rightLimit.Match(open: "open", closed: "closed"));
        }

        [Fact]
        public void InnerValue_ShouldReturnTrue()
        {
            // arrange
            var sut = new Range<decimal>(0m, 10m);
            decimal testValue = 5m;

            // act
            bool result = sut.Contains(testValue);

            // assert
            result.Should().BeTrue(because: "{0} ∊ {1}", testValue, sut);
        }

        [Fact]
        public void OuterValue_ShouldReturnFalse()
        {
            // arrange
            var sut = new Range<decimal>(0m, 10m);
            decimal testValue = 15m;

            // act
            bool result = sut.Contains(testValue);

            // assert
            result.Should().BeFalse(because: "{0} ∉ {1}", testValue, sut);
        }
    }
}