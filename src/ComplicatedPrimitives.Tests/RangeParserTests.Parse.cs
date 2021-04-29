using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeParserTests
    {
        public class Parse : RangeParserTests
        {
            public Parse(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData("[1;2]", LimitType.Closed, 1d, 2d, LimitType.Closed)]
            [InlineData("(1;2]", LimitType.Open, 1d, 2d, LimitType.Closed)]
            [InlineData("[1;2)", LimitType.Closed, 1d, 2d, LimitType.Open)]
            [InlineData("(1;2)", LimitType.Open, 1d, 2d, LimitType.Open)]
            public void LeftLimitValue_ShouldReturnExpectedResult(string str, LimitType leftLimitType, double leftLimit, double rightLimit, LimitType rightLimitType)
            {
                // arrange
                var sut = new RangeParser<double>(new DefaultValueParser());

                // act
                var result = sut.Parse(str);

                // assert
                result.Left.Type.Should().Be(leftLimitType);
                result.Left.Value.Should().Be(leftLimit);
                result.Right.Value.Should().Be(rightLimit);
                result.Right.Type.Should().Be(rightLimitType);
            }
        }
    }
}
