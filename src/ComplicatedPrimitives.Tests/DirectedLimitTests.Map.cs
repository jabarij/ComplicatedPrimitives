using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests
{
    partial class DirectedLimitTests
    {
        public class Map : DirectedLimitTests
        {
            public Map(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void Undefined_ShouldReturnUndefined()
            {
                // arrange
                var sut = DirectedLimit<int>.Undefined;
                var expected = DirectedLimit<double>.Undefined;

                // act
                DirectedLimit<double> result = sut.Map(e => (double)e);

                // assert
                result.Should().Be(expected);
            }

            [Fact]
            public void LeftInfinity_ShouldReturnLeftInfinity()
            {
                // arrange
                var sut = DirectedLimit<int>.LeftInfinity;
                var expected = DirectedLimit<double>.LeftInfinity;

                // act
                DirectedLimit<double> result = sut.Map(e => (double)e);

                // assert
                result.Should().Be(expected);
            }

            [Fact]
            public void RightInfinity_ShouldReturnRightInfinity()
            {
                // arrange
                var sut = DirectedLimit<int>.RightInfinity;
                var expected = DirectedLimit<double>.RightInfinity;

                // act
                DirectedLimit<double> result = sut.Map(e => (double)e);

                // assert
                result.Should().Be(expected);
            }

            [Theory]
            [InlineData(LimitSide.Left, LimitType.Open)]
            [InlineData(LimitSide.Left, LimitType.Closed)]
            [InlineData(LimitSide.Right, LimitType.Open)]
            [InlineData(LimitSide.Right, LimitType.Closed)]
            public void ShouldMapValue(LimitSide side, LimitType type)
            {
                // arrange
                var expectedValue = Fixture.Create<TimeSpan>();
                var sut = new DirectedLimit<long>(expectedValue.Ticks, type, side);
                var expected = new DirectedLimit<TimeSpan>(expectedValue, type, side);


                // act
                var result = sut.Map(e => new TimeSpan(e));

                // assert
                result.Should().Be(expected);
            }
        }
    }
}
