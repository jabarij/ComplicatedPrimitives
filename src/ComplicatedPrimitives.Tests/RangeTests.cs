using ComplicatedPrimitives.TestAbstractions;

namespace ComplicatedPrimitives.Tests
{
    public partial class RangeTests : TestsBase
    {
        public RangeTests(TestFixture testFixture) : base(testFixture)
        {
            Fixture.CustomizeLimitValue();
            Fixture.CustomizeDirectedLimit();
            Fixture.CustomizeRange();
        }
    }
}
