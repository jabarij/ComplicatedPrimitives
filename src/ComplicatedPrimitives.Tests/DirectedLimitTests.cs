using ComplicatedPrimitives.TestAbstractions;
using System;
using AutoFixture;

namespace ComplicatedPrimitives.Tests
{
    public partial class DirectedLimitTests : TestsBase
    {
        public DirectedLimitTests(TestFixture testFixture) : base(testFixture)
        {
            Fixture.CustomizeDirectedLimit();
        }

        protected DirectedLimit<T> Create<T>(
            T? value = null,
            LimitPointType? type = null,
            LimitSide? side = null)
            where T : struct, IComparable<T> =>
            new DirectedLimit<T>(
                value: value ?? Fixture.Create<T>(),
                type: type ?? Fixture.Create<LimitPointType>(),
                side: side ?? Fixture.CreateEnum(excluded: LimitSide.Undefined));
    }
}
