using ComplicatedPrimitives.TestAbstractions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ComplicatedPrimitives.Tests
{
    partial class RangeExtensionsTests : RangeTests
    {
        public class Merge : RangeExtensionsTests
        {
            public Merge(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Merge), nameof(GetSingleResultTestData))]
            public void ShouldReturnSingleExpectedResult(IEnumerable<Range<double>> ranges, Range<double> expected, string reason)
            {
                // arrange
                // act
                var result = RangeExtensions.Merge(ranges).ToList();

                // assert
                result.Should().HaveCount(1, because: reason, string.Join(",", ranges), expected);
                result.Single().Should().Be(expected, because: reason, string.Join(",", ranges), expected);
            }
            private static IEnumerable<SingleResultTestData> GetSingleResultTestData()
            {
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 3d),
                        new Range<double>(1d, 3d))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two equal ranges ({0}) should merge into same range ({1})");
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 3d),
                        new Range<double>(2d, 3d))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two intersecting ranges ({0}) with the same right limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 2d),
                        new Range<double>(1d, 3d))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two intersecting ranges ({0}) with the same left limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 2d, rightLimit: LimitPointType.Closed),
                        new Range<double>(2d, 3d, leftLimit: LimitPointType.Closed))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two intersecting ranges ({0}) sharing only closed limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 2d, rightLimit: LimitPointType.Open),
                        new Range<double>(2d, 3d, leftLimit: LimitPointType.Closed))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two intersecting ranges ({0}) sharing open limit with closed limit should merge into single range ({1})");
                yield return SingleResultTestData
                    .For(
                        new Range<double>(1d, 2d, rightLimit: LimitPointType.Closed),
                        new Range<double>(2d, 3d, leftLimit: LimitPointType.Open))
                    .Expect(new Range<double>(1d, 3d))
                    .Because("two intersecting ranges ({0}) sharing closed limit with open limit should merge into single range ({1})");
            }
            private class SingleResultTestData : ITestDataProvider
            {
                public IEnumerable<Range<double>> Ranges { get; set; }
                public Range<double> Expected { get; set; }
                public string Reason { get; set; }

                public static SingleResultTestData For(params Range<double>[] ranges) =>
                    new SingleResultTestData { Ranges = ranges };
                public SingleResultTestData Expect(Range<double> expected) =>
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
                    new Range<double>(1d, 2d, rightLimit: LimitPointType.Open),
                    new Range<double>(2d, 3d, leftLimit: LimitPointType.Open)
                };

                // act
                var result = RangeExtensions.Merge(ranges).ToList();

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
                        new Range<double>(e - 1, e, LimitPointType.Open , LimitPointType.Open),
                        new Range<double>(e, e + 1, LimitPointType.Open , LimitPointType.Open)
                    })
                    .ToList();

                // act
                var result = RangeExtensions.Merge(ranges).ToList();

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
                        new Range<double>(e - 1, e, LimitPointType.Closed , LimitPointType.Open),
                        new Range<double>(e, e + 1, LimitPointType.Closed , LimitPointType.Open)
                    })
                    .ToList();
                var expected = new Range<double>(
                    left: ranges.First().Left,
                    right: ranges.Last().Right);

                // act
                var result = RangeExtensions.Merge(ranges).ToList();

                // assert
                result.Should().HaveCount(1);
                result.Should().BeEquivalentTo(expected);
            }
        }
    }
}