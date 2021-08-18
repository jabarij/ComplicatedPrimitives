using AutoFixture;
using AutoFixture.Kernel;
using System;

namespace ComplicatedPrimitives.Tests
{
    public class DirectedLimitFactory<T> where T : IComparable<T>
    {
        public DirectedLimit<T> Create(ISpecimenContext context) =>
            new DirectedLimit<T>(context.Create<T>(), context.Create<LimitPointType>(), context.Create<LimitSide>());
    }
}
