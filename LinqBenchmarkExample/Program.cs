using LinqBenchmarkExample.Linq;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;
using LinqBenchmarkExample.DSimpleBenchmark;

namespace LinqBenchmarkExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            //var summary = BenchmarkRunner.Run<NameParserBenchmarks>();
            var countBenchmarks = new CountBenchmarks();
            DSBenchmark.Print(countBenchmarks.ForeachCount);
            DSBenchmark.Print(countBenchmarks.LinqCount);
            DSBenchmark.Print(countBenchmarks.LinqEnumerableCount);
            DSBenchmark.Print(countBenchmarks.LinqListCount);
#else
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
#endif
        }
    }
}
