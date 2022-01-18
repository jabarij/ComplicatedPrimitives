using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using ComplicatedPrimitives.Validation;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class LimitTypeParserTests
    {
        public class Parse : LimitTypeParserTests
        {
            public Parse(TestFixture testFixture) : base(testFixture)
            {
            }

            [Theory]
            [InlineData("[", LimitType.Closed)]
            [InlineData("(", LimitType.Open)]
            [InlineData("]", LimitType.Closed)]
            [InlineData(")", LimitType.Open)]
            public void ShouldReturnExpectedResult(string format, LimitType expected)
            {
                // arrange
                var sut = new LimitTypeParser();

                // act
                var result = sut.Parse(format);

                // assert
                result.Should().Be(expected);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void NullOrEmptyStringOrWhiteSpaces_ShouldThrow(string format)
            {
                // arrange
                var sut = new LimitTypeParser();

                // act
                Action parse = () => sut.Parse(format);

                // assert
                var exception = parse.Should().Throw<ParsingException>().And;
                exception.Format.Should().Be(format);
                exception.Message.Should().Be(ErrorMessages.UnknownFormat);
            }

            [Fact]
            public void FormatTooLong_ShouldThrow()
            {
                // arrange
                string format = "[[";
                var sut = new LimitTypeParser();

                // act
                Action parse = () => sut.Parse(format);

                // assert
                var exception = parse.Should().Throw<ParsingException>().And;
                exception.Format.Should().Be(format);
                exception.Message.Should().Be(ErrorMessages.StringTooLong(1));
            }
        }
    }
}