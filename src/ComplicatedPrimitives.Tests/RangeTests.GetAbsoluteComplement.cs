using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;
using AutoFixture;

namespace ComplicatedPrimitives.Tests;

partial class RangeTests
{
    public class GetAbsoluteComplement : RangeTests
    {
        public GetAbsoluteComplement(TestFixture testFixture) : base(testFixture) { }

        [Fact]
        public void EmptyRange_ShouldReturnSingleInfiniteRange()
        {
            // arrange
            // act
            var result = Range<double>.Empty.GetAbsoluteComplement();

            // assert
            result.Should().HaveCount(1);
            result[0].Should().Be(Range<double>.Infinite);
        }

        [Fact]
        public void InfiniteRange_ShouldReturnSingleEmptyRange()
        {
            // arrange
            // act
            var result = Range<double>.Infinite.GetAbsoluteComplement();

            // assert
            result.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void LeftInfiniteRange_ShouldReturnSingleRightInfiniteRange()
        {
            // arrange
            var sut = new Range<double>(
                left: LimitValue<double>.Infinity,
                right: Fixture.Create<LimitValue<double>>());
            var expected = new Range<double>(
                left: sut.Right.LimitValue.FlipLimitType(),
                right: LimitValue<double>.Infinity);

            // act
            var result = sut.GetAbsoluteComplement();

            // assert
            result.Should().HaveCount(1);
            result[0].Should().Be(expected);
        }

        [Fact]
        public void RightInfiniteRange_ShouldReturnSingleLeftInfiniteRange()
        {
            // arrange
            var sut = new Range<double>(
                left: Fixture.Create<LimitValue<double>>(),
                right: LimitValue<double>.Infinity);
            var expected = new Range<double>(
                left: LimitValue<double>.Infinity,
                right: sut.Left.LimitValue.FlipLimitType());

            // act
            var result = sut.GetAbsoluteComplement();

            // assert
            result.Should().HaveCount(1);
            result[0].Should().Be(expected);
        }

        [Fact]
        public void ShouldReturnSumOfTwoInfiniteComplements()
        {
            // arrange
            var sut = Fixture.Create<Range<double>>();
            var expectedLeft = new Range<double>(
                left: LimitValue<double>.Infinity,
                right: sut.Left.LimitValue.FlipLimitType());
            var expectedRight = new Range<double>(
                left: sut.Right.LimitValue.FlipLimitType(),
                right: LimitValue<double>.Infinity);

            // act
            var result = sut.GetAbsoluteComplement();

            // assert
            result.Should().HaveCount(2);
            result.Should().Contain(expectedLeft);
            result.Should().Contain(expectedRight);
        }
    }
}