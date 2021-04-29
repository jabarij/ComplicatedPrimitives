using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class IsProperSubsetOf : RangeTests
        {
            public IsProperSubsetOf(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NonEmptyRange_ShouldNotBeProperSubsetOfEmptyRange()
            {
                // arrange
                var sut = Fixture.Create<Range<double>>();

                // act
                bool result = sut.IsProperSubsetOf(Range<double>.Empty);

                // assert
                result.Should().BeFalse(because: "non-empty range cannot be proper subset of empty range ({0} ⊄ {1})", sut, Range<double>.Empty);
            }

            [Fact]
            public void EmptyRange_ShouldBeProperSubsetOfNonEmptyRange()
            {
                // arrange
                var other = Fixture.Create<Range<double>>();

                // act
                bool result = Range<double>.Empty.IsProperSubsetOf(other);

                // assert
                result.Should().BeTrue(because: "empty range is proper subset of every range ({0} ⊂ {1})", Range<double>.Empty, other);
            }

            [Fact]
            public void EmptyRange_ShouldNotBeProperSubsetOfItself()
            {
                // arrange
                // act
                bool result = Range<double>.Empty.IsProperSubsetOf(Range<double>.Empty);

                // assert
                result.Should().BeFalse(because: "empty range is not proper subset of itself ({0} ⊄ {0})", Range<double>.Empty);
            }

            [Theory]
            [InlineData(1, 3, 1, 4, true, "{1} is superset of {0} sharing left limit value")]
            [InlineData(1, 3, 0, 3, true, "{1} is superset of {0} sharing right limit value")]
            [InlineData(0, 2, 0, 2, false, "ranges are equal")]
            [InlineData(0, 2, 3, 5, false, "ranges are disjoint")]
            public void SupersetSharingLeftLimitValue_ShouldReturnTrue(double left, double right, double otherLeft, double otherRight, bool expected, string because)
            {
                // arrange
                var sut = new Range<double>(left, right);
                var other = new Range<double>(otherLeft, otherRight);

                // act
                bool result = sut.IsProperSubsetOf(other);

                // assert
                result.Should().Be(expected, because: $"{{0}} {{2}} {{1}} ({because})", sut, other, expected ? "⊂" : "⊄");
            }


            [Fact]
            public void InfiniteRange_ShouldNotBeProperSubsetOfMaximumRange()
            {
                // arrange
                var maximumRange = new Range<double>(
                    left: new LimitValue<double>(double.MinValue, LimitType.Closed),
                    right: new LimitValue<double>(double.MaxValue, LimitType.Closed));

                // act
                bool result = Range<double>.Infinite.IsProperSubsetOf(maximumRange);

                // assert
                result.Should().BeFalse(because: "infinite range is not proper subset of any finite range including the maximum one ({0} ⊄ [min;max])", Range<double>.Infinite);
            }


            [Fact]
            public void InfiniteRange_ShouldBeProperSubsetOfItself()
            {
                // arrange
                // act
                bool result = Range<double>.Infinite.IsProperSubsetOf(Range<double>.Infinite);

                // assert
                result.Should().BeFalse(because: "infinite range is not proper subset of itself ({0} ⊄ {0})", Range<double>.Infinite);
            }
        }
    }
}
