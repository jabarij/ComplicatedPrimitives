namespace ComplicatedPrimitives.Tests
{
    public class BinaryPredicateTestData<T> : BinaryOperationTestData<T, bool>
    {
        public BinaryPredicateTestData(T sut, T other, bool expected)
            : base(sut, other, expected) { }
    }
}
