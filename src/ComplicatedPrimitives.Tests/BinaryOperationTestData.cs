using ComplicatedPrimitives.TestAbstractions;

namespace ComplicatedPrimitives.Tests
{
    public class BinaryOperationTestData<T, TResult> : ITestDataProvider
    {
        public BinaryOperationTestData(
            T sut,
            T other,
            TResult expected)
        {
            Sut = sut;
            Other = other;
            Expected = expected;
        }

        public readonly T Sut;
        public readonly T Other;
        public readonly TResult Expected;

        public object[] GetTestParameters() =>
            new object[] { Sut, Other, Expected };
    }
}
