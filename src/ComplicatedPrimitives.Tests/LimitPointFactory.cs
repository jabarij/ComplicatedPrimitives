using AutoFixture;
using AutoFixture.Kernel;
using System;

namespace ComplicatedPrimitives.Tests
{
    public class LimitPointFactory<T> where T : IComparable<T>
    {
        public LimitPoint<T> Create(ISpecimenContext context) =>
            new LimitPoint<T>(
                value: context.Create<T>(),
                type: context.Create<LimitPointType>());
    }
}