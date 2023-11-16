using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests;

partial class CaseInsensitiveStringTests
{
    public class Operators : CaseInsensitiveStringTests
    {
        public Operators(TestFixture testFixture) : base(testFixture) { }

        public class Equality : Operators
        {
            public Equality(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("asdf")]
            public void ShouldIgnoreCharacterCase(string value)
            {
                // arrange
                var sut = new CaseInsensitiveString(value?.ToLower());
                var other = new CaseInsensitiveString(value?.ToUpper());

                // act
                // assert
                sut.Should().Be(other, because: "'{0}' and '{1}' are case insensitively equal", sut, other);
            }
        }

        public class ConversionToString : Operators
        {
            public ConversionToString(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("asdf")]
            [InlineData("ASDF")]
            [InlineData("ASdf")]
            public void ShouldReturnOriginalValue(string value)
            {
                // arrange
                var sut = new CaseInsensitiveString(value);

                // act
                string? result = sut;

                // assert
                result.Should().Be(value);
            }
        }
    }
}