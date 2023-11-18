using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests;

partial class RangeParserTests
{
    public class TryParse : RangeParserTests
    {
        public TryParse(TestFixture testFixture) : base(testFixture)
        {
        }

        [Theory]
        [InlineData("[1;2]", LimitType.Closed, 1, 2, LimitType.Closed)]
        [InlineData("(1;2]", LimitType.Open, 1, 2, LimitType.Closed)]
        [InlineData("[1;2)", LimitType.Closed, 1, 2, LimitType.Open)]
        [InlineData("(1;2)", LimitType.Open, 1, 2, LimitType.Open)]
        public void FiniteRange_ShouldReturnExpectedResult(
            string format,
            LimitType leftLimitType,
            int leftLimit,
            int rightLimit,
            LimitType rightLimitType
        )
        {
            // arrange
            var sut = new RangeParser<int>(new DefaultValueParser());

            // act
            var success = sut.TryParse(format, out var result);

            // assert
            success.Should().BeTrue();
            result.Left.Type.Should().Be(leftLimitType);
            result.Left.Value.Should().Be(leftLimit);
            result.Right.Value.Should().Be(rightLimit);
            result.Right.Type.Should().Be(rightLimitType);
        }

        [Theory]
        [InlineData("(1,2]", ',', LimitType.Open, 1, 2, LimitType.Closed)]
        [InlineData("(1|2]", '|', LimitType.Open, 1, 2, LimitType.Closed)]
        [InlineData("(1222]", '2', LimitType.Open, 1, 22, LimitType.Closed)]
        public void FiniteRange_CustomSeparator_ShouldReturnExpectedResult(
            string format,
            char separator,
            LimitType leftLimitType,
            int leftLimit,
            int rightLimit,
            LimitType rightLimitType
        )
        {
            // arrange
            var sut = new RangeParser<int>(new DefaultValueParser(), separator);

            // act
            var success = sut.TryParse(format, out var result);

            // assert
            success.Should().BeTrue();
            result.Left.Type.Should().Be(leftLimitType);
            result.Left.Value.Should().Be(leftLimit);
            result.Right.Value.Should().Be(rightLimit);
            result.Right.Type.Should().Be(rightLimitType);
        }

        [Theory]
        [InlineData("(-∞;0]")]
        [InlineData("(-oo;0]")]
        public void LeftInfiniteRange_ShouldReturnExpectedResult(string format)
        {
            // arrange
            var sut = new RangeParser<int>(new DefaultValueParser());

            // act
            var success = sut.TryParse(format, out var result);

            // assert
            success.Should().BeTrue();
            result.Left.Type.Should().Be(LimitType.Open);
            result.Left.LimitValue.IsInfinity.Should().BeTrue();
            result.Right.Value.Should().Be(0);
            result.Right.Type.Should().Be(LimitType.Closed);
        }

        [Theory]
        [InlineData("[0;∞)")]
        [InlineData("[0;oo)")]
        [InlineData("[0;+∞)")]
        [InlineData("[0;+oo)")]
        public void RightInfiniteRange_ShouldReturnExpectedResult(string format)
        {
            // arrange
            var sut = new RangeParser<int>(new DefaultValueParser());

            // act
            var success = sut.TryParse(format, out var result);

            // assert
            success.Should().BeTrue();
            result.Left.Value.Should().Be(0);
            result.Left.Type.Should().Be(LimitType.Closed);
            result.Right.Type.Should().Be(LimitType.Open);
            result.Right.LimitValue.IsInfinity.Should().BeTrue();
        }

        [Theory]
        [InlineData("/1,2)")]
        [InlineData("(1,2\\")]
        [InlineData("(1,2)")]
        public void InvalidValue_ShouldReturnExpectedException(string format)
        {
            // arrange
            var sut = new RangeParser<double>(new DefaultValueParser());

            // act
            var success = sut.TryParse(format, out _);

            // assert
            success.Should().BeFalse();
        }
    }
}