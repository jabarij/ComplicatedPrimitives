using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests;

partial class LimitValueTests
{
    public class Map : LimitValueTests
    {
        public Map(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void Infinity_ShouldReturnInfinity()
        {
            // arrange
            var sut = LimitValue<int>.Infinity;
            var expected = LimitValue<double>.Infinity;

            // act
            LimitValue<double> result = sut.Map(e => (double)e);

            // assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(LimitType.Open)]
        [InlineData(LimitType.Closed)]
        public void ShouldMapValue(LimitType type)
        {
            // arrange
            var expectedValue = Fixture.Create<TimeSpan>();
            var sut = new LimitValue<long>(expectedValue.Ticks, type);
            var expected = new LimitValue<TimeSpan>(expectedValue, type);


            // act
            var result = sut.Map(e => new TimeSpan(e));

            // assert
            result.Should().Be(expected);
        }
    }
}