using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class GetIntersection : RangeTests
        {
            public GetIntersection(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void SelfIntersection_ShouldBeSelfEqual()
            {
                // arrange
                var range = Fixture.Create<Range<int>>();

                // act
                var result = range.GetIntersection(range);

                // assert
                result.Should().Be(range);
            }

            [Fact]
            public void ShouldReturnExpectedResult()
            {
                // arrange
                // [
                var sut = new Range<decimal>(
                    left: new LimitPoint<decimal>(1m, LimitPointType.Open),
                    right: new LimitPoint<decimal>(3m, LimitPointType.Closed));
                var other = new Range<decimal>(
                    left: new LimitPoint<decimal>(2m, LimitPointType.Open),
                    right: new LimitPoint<decimal>(4m, LimitPointType.Closed));
                var expected = new Range<decimal>(
                    left: new LimitPoint<decimal>(2m, LimitPointType.Open),
                    right: new LimitPoint<decimal>(3m, LimitPointType.Closed));

                // act
                var result = sut.GetIntersection(other);

                // assert
                result.Should().Be(expected, because: "{0} ∩ {1} should be {2}", sut, other, expected);
            }

            [Fact]
            public void ShouldBeCommutative()
            {
                // arrange
                var range1 = Fixture.Create<Range<decimal>>();
                var range2 = Fixture.Create<Range<decimal>>();

                // act
                var result1 = range1.GetIntersection(range2);
                var result2 = range2.GetIntersection(range1);

                // assert
                result1.Should().Be(result2);
            }

            [Fact]
            public void Empty_ShouldReturnEmptyResult()
            {
                // arrange
                var sut = Range<decimal>.Empty;
                var other = Fixture.Create<Range<decimal>>();

                // act
                var result = sut.GetIntersection(other);

                // assert
                result.Should().Be(Range<decimal>.Empty, because: "{0} ∩ {1} should be {2}", sut, other, Range<decimal>.Empty);
            }

            [Fact]
            public void Infinity_ShouldReturnOther()
            {
                // arrange
                var sut = Range<decimal>.Infinite;
                var other = Fixture.Create<Range<decimal>>();

                // act
                var result = sut.GetIntersection(other);

                // assert
                result.Should().Be(other, because: "{0} ∩ {1} should be {2}", sut, other, other);
            }
        }
    }
}