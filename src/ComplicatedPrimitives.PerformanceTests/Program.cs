// See https://aka.ms/new-console-template for more information

using RangeParserBenchmark = ComplicatedPrimitives.PerformanceTests.RangeParserTests.Benchmark;
using BenchmarkDotNet.Running;

var results = BenchmarkRunner.Run<RangeParserBenchmark>();