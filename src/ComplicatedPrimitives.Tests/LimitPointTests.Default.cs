using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class LimitPointTests
    {
        public class Default : LimitPointTests
        {
            public Default(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            [Requirement]
            public void IsInfinite_ShouldBeTrue()
            {
                // arrange
                // act
                var @default = default(LimitPoint<string>);

                // assert
                @default.IsInfinite.Should().BeTrue();
            }

            [Fact]
            [Requirement]
            public void IsFinite_ShouldBeFalse()
            {
                // arrange
                // act
                var @default = default(LimitPoint<string>);

                // assert
                @default.IsFinite.Should().BeFalse();
            }

            [Fact]
            [Requirement]
            public void Value_ShouldBeDefault()
            {
                // arrange
                // act
                var @default = default(LimitPoint<string>);

                // assert
                @default.Value.Should().Be(default(string));
            }

            [Fact]
            [Requirement]
            public void Type_ShouldBeOpen()
            {
                // arrange
                // act
                var @default = default(LimitPoint<string>);

                // assert
                @default.Type.Should().Be(LimitPointType.Open);
            }
        }
    }
}
