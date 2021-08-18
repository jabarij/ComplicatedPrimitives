using ComplicatedPrimitives.TestAbstractions;

namespace ComplicatedPrimitives.Tests
{
    public partial class RangeParserTests : TestsBase
    {
        public RangeParserTests(TestFixture testFixture) : base(testFixture)
        {
            Fixture.CustomizeLimitPoint();
            Fixture.CustomizeDirectedLimit();
            Fixture.CustomizeRange();
        }
    }
}
