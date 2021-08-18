using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests
{
    partial class LimitPointTests
    {
        public class Map : LimitPointTests
        {
            public Map(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void Infinity_ShouldReturnInfinity()
            {
                // arrange
                var sut = LimitPoint<int>.Infinity;
                var expected = LimitPoint<double>.Infinity;

                // act
                LimitPoint<double> result = sut.Map(e => (double)e);

                // assert
                result.Should().Be(expected);
            }

            [Theory]
            [InlineData(LimitPointType.Open)]
            [InlineData(LimitPointType.Closed)]
            public void ShouldMapValue(LimitPointType type)
            {
                // arrange
                var expectedValue = Fixture.Create<TimeSpan>();
                var sut = new LimitPoint<long>(expectedValue.Ticks, type);
                var expected = new LimitPoint<TimeSpan>(expectedValue, type);


                // act
                var result = sut.Map(e => new TimeSpan(e));

                // assert
                result.Should().Be(expected);
            }
        }
    }
}
