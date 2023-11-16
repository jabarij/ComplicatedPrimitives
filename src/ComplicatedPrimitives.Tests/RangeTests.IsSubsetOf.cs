using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests;

partial class RangeTests
{
    public class IsSubsetOf : RangeTests
    {
        public IsSubsetOf(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void NonEmptyRange_ShouldNotBeSubsetOfEmptyRange()
        {
            // arrange
            var sut = Fixture.Create<Range<double>>();

            // act
            bool result = sut.IsSubsetOf(Range<double>.Empty);

            // assert
            result.Should().BeFalse(because: "non-empty range cannot be subset of empty range ({0} ⊈ {1})", sut, Range<double>.Empty);
        }

        [Fact]
        public void EmptyRange_ShouldBeSubsetOfNonEmptyRange()
        {
            // arrange
            var other = Fixture.Create<Range<double>>();

            // act
            bool result = Range<double>.Empty.IsSubsetOf(other);

            // assert
            result.Should().BeTrue(because: "empty range is subset of every range ({0} ⊆ {1})", Range<double>.Empty, other);
        }

        [Fact]
        public void EmptyRange_ShouldBeSubsetOfItself()
        {
            // arrange
            // act
            bool result = Range<double>.Empty.IsSubsetOf(Range<double>.Empty);

            // assert
            result.Should().BeTrue(because: "empty range is subset of every range including itself ({0} ⊆ {0})", Range<double>.Empty);
        }

        [Theory]
        [InlineData(1, 3, 1, 4, true, "{1} is superset of {0} sharing left limit value")]
        [InlineData(1, 3, 0, 3, true, "{1} is superset of {0} sharing right limit value")]
        [InlineData(0, 2, 0, 2, true, "ranges are equal")]
        [InlineData(0, 2, 3, 5, false, "ranges are disjoint")]
        public void SupersetSharingLeftLimitValue_ShouldReturnTrue(double left, double right, double otherLeft, double otherRight, bool expected, string because)
        {
            // arrange
            var sut = new Range<double>(left, right);
            var other = new Range<double>(otherLeft, otherRight);

            // act
            bool result = sut.IsSubsetOf(other);

            // assert
            result.Should().Be(expected, because: $"{{0}} {{2}} {{1}} ({because})", sut, other, expected ? "⊆" : "⊈");
        }


        [Fact]
        public void InfiniteRange_ShouldNotBeSubsetOfMaximumRange()
        {
            // arrange
            var maximumRange = new Range<double>(
                left: new LimitValue<double>(double.MinValue, LimitType.Closed),
                right: new LimitValue<double>(double.MaxValue, LimitType.Closed));

            // act
            bool result = Range<double>.Infinite.IsSubsetOf(maximumRange);

            // assert
            result.Should().BeFalse(because: "infinite range is not subset of any finite range including the maximum one ({0} ⊈ [min;max])", Range<double>.Infinite);
        }


        [Fact]
        public void InfiniteRange_ShouldBeSubsetOfItself()
        {
            // arrange
            // act
            bool result = Range<double>.Infinite.IsSubsetOf(Range<double>.Infinite);

            // assert
            result.Should().BeTrue(because: "infinite range is subset of itself ({0} ⊆ {0})", Range<double>.Infinite);
        }
    }
}