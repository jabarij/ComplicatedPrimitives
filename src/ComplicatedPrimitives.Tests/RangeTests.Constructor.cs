using AutoFixture;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeTests
    {
        public class Constructor : RangeTests
        {
            public Constructor(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void LeftLimitGreaterThanRightLimit_ShouldThrow()
            {
                // arrange
                var left = Fixture.Create<DirectedLimit<int>>().With(side: LimitSide.Left);
                var right = Fixture.CreateLowerThan(left).With(side: LimitSide.Right);
                Action create = () => new Range<int>(left, right);

                // act
                // assert
                var exception = create.Should().Throw<ArgumentException>().And;
                exception.ParamName.Should().Be("left");
            }

            [Fact]
            public void ShouldCreateWithGivenValues()
            {
                // arrange
                int left = Fixture.Create<int>();
                int right = Fixture.CreateGreaterThan(left);
                var leftLimit = Fixture.Create<LimitPointType>();
                var rightLimit = Fixture.Create<LimitPointType>();

                // act
                var sut = new Range<int>(left, right, leftLimit, rightLimit);

                // assert
                sut.Left.Value.Should().Be(left);
                sut.Right.Value.Should().Be(right);
                sut.Left.Type.Should().Be(leftLimit);
                sut.Right.Type.Should().Be(rightLimit);
            }

            [Fact]
            public void LeftInfiniteLimit_ShouldCreateLeftInfiniteRange()
            {
                // arrange
                var left = DirectedLimit<double>.LeftInfinity;
                var right = Fixture.Create<DirectedLimit<double>>().With(side: LimitSide.Right);

                // act
                var sut = new Range<double>(left, right);

                // assert
                sut.IsInfiniteLeft.Should().BeTrue();
                sut.IsInfiniteRight.Should().BeFalse();
            }

            [Fact]
            public void RightInfiniteLimit_ShouldCreateRightInfiniteRange()
            {
                // arrange
                var left = Fixture.Create<DirectedLimit<double>>().With(side: LimitSide.Left);
                var right = DirectedLimit<double>.RightInfinity;

                // act
                var sut = new Range<double>(left, right);

                // assert
                sut.IsInfiniteRight.Should().BeTrue();
                sut.IsInfiniteLeft.Should().BeFalse();
            }
        }
    }
}
