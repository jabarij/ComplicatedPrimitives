using AutoFixture.Kernel;
using System;
using System.Linq;

namespace ComplicatedPrimitives.Tests
{
    public class RangeFactory<T> where T : IComparable<T>
    {
        public Range<T> Create(ISpecimenContext context)
        {
            var limits = new[] { (T)context.Resolve(typeof(T)), (T)context.Resolve(typeof(T)) }.OrderBy(e => e).ToList();
            return new Range<T>(limits[0], limits[1]);
        }
    }
}