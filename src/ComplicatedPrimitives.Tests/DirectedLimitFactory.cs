using AutoFixture;
using AutoFixture.Kernel;
using ComplicatedPrimitives.TestAbstractions;
using System;

namespace ComplicatedPrimitives.Tests
{
    public class DirectedLimitFactory<T> where T : IComparable<T>
    {
        public DirectedLimit<T> Create(ISpecimenContext context)
        {
            var value = context.Create<T>();
            var type = context.Create<LimitPointType>();
            var side = context.CreateEnum(LimitSide.Undefined);
            return new DirectedLimit<T>(value, type, side);
        }
    }
}
