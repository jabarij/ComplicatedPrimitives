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
                var left = Fixture.Create<DirectedLimit<int>>();
                var right = Fixture.CreateLowerThan(left);
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
                int to = Fixture.CreateGreaterThan(left);
                var leftLimit = Fixture.Create<LimitType>();
                var toLimit = Fixture.Create<LimitType>();

                // act
                var sut = new Range<int>(left, to, leftLimit, toLimit);

                // assert
                sut.Left.Value.Should().Be(left);
                sut.Right.Value.Should().Be(to);
                sut.Left.Type.Should().Be(leftLimit);
                sut.Right.Type.Should().Be(toLimit);
            }
        }
    }
}
