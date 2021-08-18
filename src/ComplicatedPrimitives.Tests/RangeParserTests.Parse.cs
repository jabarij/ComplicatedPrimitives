using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeParserTests
    {
        public class Parse : RangeParserTests
        {
            public Parse(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData("[1;2]", LimitPointType.Closed, 1d, 2d, LimitPointType.Closed)]
            [InlineData("(1;2]", LimitPointType.Open, 1d, 2d, LimitPointType.Closed)]
            [InlineData("[1;2)", LimitPointType.Closed, 1d, 2d, LimitPointType.Open)]
            [InlineData("(1;2)", LimitPointType.Open, 1d, 2d, LimitPointType.Open)]
            public void ShouldReturnExpectedResult(string str, LimitPointType leftPointType, double leftValue, double rightValue, LimitPointType rightPointType)
            {
                // arrange
                var sut = new RangeParser<double>(new DefaultValueParser());

                // act
                var result = sut.Parse(str);

                // assert
                result.Left.Type.Should().Be(leftPointType);
                result.Left.Value.Should().Be(leftValue);
                result.Right.Value.Should().Be(rightValue);
                result.Right.Type.Should().Be(rightPointType);
            }
        }
    }
}
