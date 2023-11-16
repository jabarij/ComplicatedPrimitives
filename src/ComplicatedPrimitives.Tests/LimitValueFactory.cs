using AutoFixture;
using AutoFixture.Kernel;
using System;

namespace ComplicatedPrimitives.Tests;

public class LimitValueFactory<T> where T : IComparable<T>
{
    public LimitValue<T> Create(ISpecimenContext context) =>
        new(
            value: context.Create<T>(),
            type: context.Create<LimitType>());
}