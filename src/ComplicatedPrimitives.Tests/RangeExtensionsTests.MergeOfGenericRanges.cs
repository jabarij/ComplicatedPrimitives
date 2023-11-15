using System;
using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeExtensionsTests
    {
        public class MergeOfGenericRanges : RangeExtensionsTests
        {
            public MergeOfGenericRanges(TestFixture testFixture) : base(testFixture)
            {
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(MergeOfGenericRanges), nameof(GetSingleResultTestData))]
            public void ShouldReturnSingleExpectedResult(
                IEnumerable<RangeOfDecimals> ranges, RangeOfDecimals expected,
                string reason
            )
            {
                // arrange
                // act
                var result = RangeExtensions.Merge<RangeOfDecimals, decimal>(ranges).ToList();

                // assert
                result.Should().HaveCount(1, because: reason, string.Join(",", ranges), expected);
                result.Single().Should().Be(expected, because: reason, string.Join(",", ranges), expected);
            }

            private static IEnumerable<SingleResultTestData> GetSingleResultTestData()
            {
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 3m),
                        new RangeOfDecimals(1m, 3m))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because("two equal ranges ({0}) should merge into same range ({1})");
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 3m),
                        new RangeOfDecimals(2m, 3m))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because(
                        "two intersecting ranges ({0}) with the same right limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 2m),
                        new RangeOfDecimals(1m, 3m))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because(
                        "two intersecting ranges ({0}) with the same left limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 2m, rightLimit: LimitType.Closed),
                        new RangeOfDecimals(2m, 3m, leftLimit: LimitType.Closed))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because(
                        "two intersecting ranges ({0}) sharing only closed limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 2m, rightLimit: LimitType.Open),
                        new RangeOfDecimals(2m, 3m, leftLimit: LimitType.Closed))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because(
                        "two intersecting ranges ({0}) sharing open limit with closed limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new RangeOfDecimals(1m, 2m, rightLimit: LimitType.Closed),
                        new RangeOfDecimals(2m, 3m, leftLimit: LimitType.Open))
                    .Expect(new RangeOfDecimals(1m, 3m))
                    .Because(
                        "two intersecting ranges ({0}) sharing closed limit with open limit should merge into single range ({1})");
            }

            private class SingleResultTestData : ITestDataProvider
            {
                public IEnumerable<RangeOfDecimals> Ranges { get; set; }
                public RangeOfDecimals Expected { get; set; }
                public string Reason { get; set; }

                public static SingleResultTestData For(params RangeOfDecimals[] ranges) =>
                    new SingleResultTestData { Ranges = ranges };

                public SingleResultTestData Expect(RangeOfDecimals expected) =>
                    new SingleResultTestData { Ranges = Ranges, Expected = expected, Reason = Reason };

                public SingleResultTestData Because(string because) =>
                    new SingleResultTestData { Ranges = Ranges, Expected = Expected, Reason = because };

                public object[] GetTestParameters() =>
                    new object[] { Ranges, Expected, Reason };
            }

            [Fact]
            public void TwoRangesNeighbouringWithOpenLimits_ShouldNotMerge()
            {
                // arrange
                var ranges = new[]
                {
                    new RangeOfDecimals(1m, 2m, rightLimit: LimitType.Open),
                    new RangeOfDecimals(2m, 3m, leftLimit: LimitType.Open)
                };

                // act
                var result = RangeExtensions.Merge<RangeOfDecimals, decimal>(ranges).ToList();

                // assert
                result.Should().HaveCount(2);
                result.Should().BeEquivalentTo(ranges);
            }

            [Fact]
            public void ManyDisjointRanges_ShouldNotMerge()
            {
                // arrange
                var ranges = Enumerable.Range(1, 10)
                    .Select(e => e * 2)
                    .SelectMany(e => new[]
                    {
                        new RangeOfDecimals(e - 1, e, LimitType.Open, LimitType.Open),
                        new RangeOfDecimals(e, e + 1, LimitType.Open, LimitType.Open)
                    })
                    .ToList();

                // act
                var result = RangeExtensions.Merge<RangeOfDecimals, decimal>(ranges).ToList();

                // assert
                result.Should().HaveCount(ranges.Count);
                result.Should().BeEquivalentTo(ranges);
            }

            [Fact]
            public void ManyIntersectingRanges_ShouldMerge()
            {
                // arrange
                var ranges = Enumerable.Range(1, 10)
                    .Select(e => e * 2)
                    .SelectMany(e => new[]
                    {
                        new RangeOfDecimals(e - 1, e, LimitType.Closed, LimitType.Open),
                        new RangeOfDecimals(e, e + 1, LimitType.Closed, LimitType.Open)
                    })
                    .ToList();
                var expected = new RangeOfDecimals(
                    left: ranges.First().Left,
                    right: ranges.Last().Right);

                // act
                var result = RangeExtensions.Merge<RangeOfDecimals, decimal>(ranges).ToList();

                // assert
                result.Should().HaveCount(1);
                result.Should().BeEquivalentTo(expected);
            }

            public class RangeOfDecimals : IRange<RangeOfDecimals, decimal>
            {
                private readonly Range<decimal> _range;

                private RangeOfDecimals(Range<decimal> range)
                {
                    _range = range;
                }

                public RangeOfDecimals(DirectedLimit<decimal> left, DirectedLimit<decimal> right)
                {
                    _range = new Range<decimal>(left, right);
                }

                public RangeOfDecimals(decimal left, decimal right)
                {
                    _range = new Range<decimal>(left, right);
                }

                public RangeOfDecimals(decimal left, decimal right, LimitType leftLimit = LimitType.Open,
                    LimitType rightLimit = LimitType.Open)
                {
                    _range = new Range<decimal>(left, right, leftLimit, rightLimit);
                }

                public bool Equals(RangeOfDecimals other)
                {
                    return _range.Equals(other._range);
                }

                public override bool Equals(object obj)
                    => obj is RangeOfDecimals other
                       && Equals(other);

                public override int GetHashCode()
                    => _range.GetHashCode();

                public DirectedLimit<decimal> Left => _range.Left;
                public DirectedLimit<decimal> Right => _range.Right;
                public decimal LeftValue => _range.LeftValue;

                public decimal RightValue => _range.RightValue;

                public bool IsInfinite => _range.IsInfinite;

                public bool IsEmpty => _range.IsEmpty;

                public bool Contains(decimal value)
                {
                    return _range.Contains(value);
                }

                public bool Intersects(RangeOfDecimals other)
                {
                    return _range.Intersects(other._range);
                }

                public bool IsSubsetOf(RangeOfDecimals other)
                {
                    return _range.IsSubsetOf(other._range);
                }

                public bool IsProperSubsetOf(RangeOfDecimals other)
                {
                    return _range.IsProperSubsetOf(other._range);
                }

                public bool IsSupersetOf(RangeOfDecimals other)
                {
                    return _range.IsSupersetOf(other._range);
                }

                public bool IsProperSupersetOf(RangeOfDecimals other)
                {
                    return _range.IsProperSupersetOf(other._range);
                }

                public RangeOfDecimals GetIntersection(RangeOfDecimals other)
                {
                    return new RangeOfDecimals(_range.GetIntersection(other._range));
                }

                public IEnumerable<RangeOfDecimals> GetUnion(RangeOfDecimals other)
                {
                    return _range.GetUnion(other._range).Select(e => new RangeOfDecimals(e));
                }

                public IEnumerable<RangeOfDecimals> GetComplementIn(RangeOfDecimals other)
                {
                    return _range.GetComplementIn(other._range).Select(e => new RangeOfDecimals(e));
                }

                public IEnumerable<RangeOfDecimals> GetAbsoluteComplement()
                {
                    return _range.GetAbsoluteComplement().Select(e => new RangeOfDecimals(e));
                }
            }
        }
    }
}