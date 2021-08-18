using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class IsSupersetOf : RangeTests
        {
            public IsSupersetOf(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NonEmptyRange_ShouldBeSupersetOfEmptyRange()
            {
                // arrange
                var sut = Fixture.Create<Range<double>>();

                // act
                bool result = sut.IsSupersetOf(Range<double>.Empty);

                // assert
                result.Should().BeTrue(because: "any non-empty range is superset of empty range ({0} ⊇ {1})", sut, Range<double>.Empty);
            }

            [Fact]
            public void EmptyRange_ShouldNotBeSupersetOfNonEmptyRange()
            {
                // arrange
                var other = Fixture.Create<Range<double>>();

                // act
                bool result = Range<double>.Empty.IsSupersetOf(other);

                // assert
                result.Should().BeFalse(because: "empty range is not superset of any non-empty range ({0} ⊉ {1})", Range<double>.Empty, other);
            }

            [Fact]
            public void EmptyRange_ShouldBeSupersetOfItself()
            {
                // arrange
                // act
                bool result = Range<double>.Empty.IsSupersetOf(Range<double>.Empty);

                // assert
                result.Should().BeTrue(because: "empty range is superset of itself ({0} ⊇ {0})", Range<double>.Empty);
            }

            [Theory]
            [InlineData(1, 4, 1, 3, true, "{1} is subset of {0} sharing left limit value")]
            [InlineData(0, 3, 1, 3, true, "{1} is subset of {0} sharing right limit value")]
            [InlineData(0, 2, 0, 2, true, "ranges are equal")]
            [InlineData(3, 5, 0, 2, false, "ranges are disjoint")]
            public void ShouldReturnExpectedResult(double left, double right, double otherLeft, double otherRight, bool expected, string because)
            {
                // arrange
                var sut = new Range<double>(left, right);
                var other = new Range<double>(otherLeft, otherRight);

                // act
                bool result = sut.IsSupersetOf(other);

                // assert
                result.Should().Be(expected, because: $"{{0}} {{2}} {{1}} ({because})", sut, other, expected ? "⊇" : "⊉");
            }


            [Fact]
            public void InfiniteRange_ShouldBeSupersetOfMaximumRange()
            {
                // arrange
                var maximumRange = new Range<double>(
                    left: new LimitPoint<double>(double.MinValue, LimitPointType.Closed),
                    right: new LimitPoint<double>(double.MaxValue, LimitPointType.Closed));

                // act
                bool result = Range<double>.Infinite.IsSupersetOf(maximumRange);

                // assert
                result.Should().BeTrue(because: "infinite range is superset of every finite range including the maximum one ({0} ⊇ [min;max])", Range<double>.Infinite);
            }


            [Fact]
            public void InfiniteRange_ShouldBeSupersetOfItself()
            {
                // arrange
                // act
                bool result = Range<double>.Infinite.IsSupersetOf(Range<double>.Infinite);

                // assert
                result.Should().BeTrue(because: "infinite range is superset of itself ({0} ⊇ {0})", Range<double>.Infinite);
            }
        }
    }
}
