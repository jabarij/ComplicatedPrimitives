using ComplicatedPrimitives.TestAbstractions;
using Xunit;
using AutoFixture;
using FluentAssertions;

namespace ComplicatedPrimitives.Tests;

partial class DirectedLimitTests
{
    public class Deconstruct : DirectedLimitTests
    {
        public Deconstruct(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void LeftLimitValue_ShouldReturnExpectedResult()
        {
            // arrange
            var sut = Fixture.Create<DirectedLimit<decimal>>();

            // act
            var (limitValue, side) = sut;

            // assert
            limitValue.Should().Be(sut.LimitValue);
            side.Should().Be(sut.Side);
        }
    }
}