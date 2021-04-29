using ComplicatedPrimitives.TestAbstractions;

namespace ComplicatedPrimitives.Tests
{
    public partial class DirectedLimitTests : TestsBase
    {
        public DirectedLimitTests(TestFixture testFixture) : base(testFixture)
        {
            Fixture.CustomizeDirectedLimit();
        }
    }
}
