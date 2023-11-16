using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests;

partial class DirectedLimitTests
{
    public class GetComplement : DirectedLimitTests
    {
        public GetComplement(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void ShouldReturnExpectedResult()
        {
            // arrange
            var sut = Fixture.Create<DirectedLimit<decimal>>();

            // act
            var result = sut.GetComplement();

            // assert
            result.Value.Should().Be(sut.Value);
            result.Type.Should().Be(sut.Type.Flip());
            result.Side.Should().Be(sut.Side.Flip());
        }
    }
}