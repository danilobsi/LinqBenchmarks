using BenchmarkAndSpanExample.Linq;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;

namespace BenchmarkAndSpanExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            //var summary = BenchmarkRunner.Run<NameParserBenchmarks>();
            var countBenchmarks = new CountBenchmarks();
            Print(countBenchmarks.ForeachCount);
            Print(countBenchmarks.LinqCount);
            Print(countBenchmarks.LinqEnumerableCount);
            Print(countBenchmarks.LinqListCount);
#else
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
#endif
        }

        static void Print<T>(Func<T> functionToMeasure)
        {
            //Warm up
            for (var i = 0; i < 50; i++)
                functionToMeasure();

            double average = 0;
            //Measure
            for (var i = 0; i < 1000; i++) {
                var watch = new Stopwatch();

                watch.Start();
                functionToMeasure();
                watch.Stop();

                average = ((average * i) + watch.ElapsedTicks) / (i + 1);
            }
            Console.Out.WriteLine($"{functionToMeasure.Method.Name}: {functionToMeasure()} - avg: {average} ticks");
        }
    }
}
