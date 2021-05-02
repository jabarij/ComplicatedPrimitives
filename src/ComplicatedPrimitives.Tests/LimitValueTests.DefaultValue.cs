using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class LimitValueTests
    {
        public class DefaultValue : LimitValueTests
        {
            public DefaultValue(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            [Requirement]
            public void DefaultValue_ShouldBeInfinite()
            {
                // arrange
                // act
                var defaultValue = default(LimitValue<string>);

                // assert
                defaultValue.IsInfinite.Should().BeTrue();
            }

            [Fact]
            [Requirement]
            public void DefaultValue_ShouldHaveDefaultValue()
            {
                // arrange
                // act
                var defaultValue = default(LimitValue<string>);

                // assert
                defaultValue.Value.Should().Be(default(string));
            }

            [Fact]
            [Requirement]
            public void DefaultValue_ShouldHaveOpenType()
            {
                // arrange
                // act
                var defaultValue = default(LimitValue<string>);

                // assert
                defaultValue.Type.Should().Be(LimitType.Open);
            }
        }
    }
}
