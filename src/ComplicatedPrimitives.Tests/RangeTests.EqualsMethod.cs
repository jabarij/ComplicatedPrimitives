using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class EqualsMethod : RangeTests
        {
            public EqualsMethod(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void InfiniteRange_ShouldBeEqualItself()
            {
                // arrange
                // act
                bool result = Range<int>.Infinite.Equals(Range<int>.Infinite);

                // assert
                result.Should().BeTrue();
            }

            [Fact]
            public void InfiniteRange_ShouldNotBeEqualMaximumRange()
            {
                // arrange
                var maximumRange = new Range<int>(
                    left: new LimitPoint<int>(int.MinValue, LimitPointType.Closed),
                    right: new LimitPoint<int>(int.MaxValue, LimitPointType.Closed));

                // act
                bool result = Range<int>.Infinite.Equals(maximumRange);

                // assert
                result.Should().BeFalse();
            }

            [Fact]
            public void EmptyRange_ShouldBeEqualItself()
            {
                // arrange
                // act
                bool result = Range<int>.Empty.Equals(Range<int>.Empty);

                // assert
                result.Should().BeTrue();
            }

            [Fact]
            public void OtherIsNull_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();

                // act
                bool result = sut.Equals(null);

                // assert
                result.Should().BeFalse();
            }

            [Fact]
            public void SameLimits_ShouldReturnTrue()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();
                var other = new Range<int>(sut.Left, sut.Right);

                // act
                // assert
                sut.Equals(other).Should().BeTrue();
                other.Equals(sut).Should().BeTrue();
            }

            [Fact]
            public void DifferentLeftValue_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();
                var other = new Range<int>(Fixture.CreateLowerThan(sut.Left), sut.Right);

                // act
                // assert
                sut.Equals(other).Should().BeFalse();
                other.Equals(sut).Should().BeFalse();
            }

            [Fact]
            public void DifferentRightValue_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();
                var other = new Range<int>(sut.Left, Fixture.CreateGreaterThan(sut.Right));

                // act
                // assert
                sut.Equals(other).Should().BeFalse();
                other.Equals(sut).Should().BeFalse();
            }

            [Fact]
            public void DifferentLeftLimitType_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();
                var other = new Range<int>(sut.Left.FlipLimitType(), sut.Right);

                // act
                // assert
                sut.Equals(other).Should().BeFalse();
                other.Equals(sut).Should().BeFalse();
            }

            [Fact]
            public void DifferentRightLimitType_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.CreateRange<int>();
                var other = new Range<int>(sut.Left, sut.Right.FlipLimitType());

                // act
                // assert
                sut.Equals(other).Should().BeFalse();
                other.Equals(sut).Should().BeFalse();
            }
        }
    }
}
