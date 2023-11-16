using AutoFixture;
using AutoFixture.Kernel;
using System;

namespace ComplicatedPrimitives.Tests;

public class DirectedLimitFactory<T> where T : IComparable<T>
{
    public DirectedLimit<T> Create(ISpecimenContext context) => new(context.Create<T>(), context.Create<LimitType>(),
        context.Create<LimitSide>());
}