using ComplicatedPrimitives.TestAbstractions;

namespace ComplicatedPrimitives.Tests;

public partial class CaseInsensitiveStringTests : TestsBase
{
    public CaseInsensitiveStringTests(TestFixture testFixture) : base(testFixture)
    {
        Fixture.CustomizeCaseInsensitiveString();
    }
}