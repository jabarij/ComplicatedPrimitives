using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests;

partial class RangeTests
{
    public class Map : RangeTests
    {
        public Map(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void Empty_ShouldReturnEmpty()
        {
            // arrange
            var sut = Range<int>.Empty;
            var expected = Range<double>.Empty;

            // act
            Range<double> result = sut.Map(e => (double)e);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Infinite_ShouldReturnInfinite()
        {
            // arrange
            var sut = Range<int>.Infinite;
            var expected = Range<double>.Infinite;

            // act
            Range<double> result = sut.Map(e => (double)e);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public void ShouldMapValue()
        {
            // arrange
            var expectedLeftValue = Fixture.Create<TimeSpan>();
            var expectedRightValue = expectedLeftValue + Fixture.Create<TimeSpan>();
            var sut = new Range<long>(expectedLeftValue.Ticks, expectedRightValue.Ticks);
            var expected = new Range<TimeSpan>(expectedLeftValue, expectedRightValue, sut.Left.Type, sut.Right.Type);

            // act
            var result = sut.Map(e => new TimeSpan(e));

            // assert
            result.Should().Be(expected);
        }
    }
}