using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class Deconstruct : RangeTests
        {
            public Deconstruct(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void LeftLimitValue_ShouldReturnExpectedResult()
            {
                // arrange
                var sut = Fixture.CreateRange<decimal>();

                // act
                var (left, right) = sut;

                // assert
                left.Should().Be(sut.Left);
                right.Should().Be(sut.Right);
            }
        }
    }
}
