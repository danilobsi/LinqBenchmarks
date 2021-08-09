using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System.Collections.Generic;
using System.Linq;

namespace LinqBenchmarkExample.Linq
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    //[SimpleJob(RunStrategy.Monitoring, targetCount: 100)]
    [ShortRunJob]
    public class FilterBenchmarks
    {
        Sample[] samples;

        [Params(1000)]
        public int arraySize { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            samples = new Sample[arraySize];
            for (var i = 0; i < arraySize; i++)
                samples[i] = new Sample
                {
                    Id = i,
                    Name = "SayMyName"
                };
        }

#if DEBUG
        public FilterBenchmarks()
        {
            arraySize = 1000;
            GlobalSetup();
        }
#endif

        [Benchmark(Baseline = true)]
        public void ListToForeachFilter()
        {
            var items = samples.Where(s => s.Id % 2 == 1).ToList();
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        [Benchmark]
        public void EnumerableToForeachFilter()
        {
            var items = samples.Where(s => s.Id % 2 == 1);
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        [Benchmark]
        public void YieldToForeachFilter()
        {
            var items = YieldFilterOddNumbers();
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        [Benchmark]
        public void ForeachToForeachFilter()
        {
            var items = ForeachFilterOddNumbers();
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        [Benchmark]
        public void EnumerableForeachTwice()
        {
            var items = samples.Where(s => s.Id % 2 == 1);
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };

            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        [Benchmark]
        public void ListForeachTwice()
        {
            var items = samples.Where(s => s.Id % 2 == 1).ToList();
            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };

            foreach (var sample in items)
                _ = new Sample { Name = sample.Name };
        }

        private IEnumerable<Sample> ForeachFilterOddNumbers()
        {
            var items = new List<Sample>();
            foreach (var sample in samples)
            {
                if (sample.Id % 2 == 1)
                {
                    items.Add(sample);
                }
            }

            return items;
        }

        private IEnumerable<Sample> YieldFilterOddNumbers()
        {
            foreach (var sample in samples)
            {
                if (sample.Id % 2 == 1)
                {
                    yield return sample;
                }
            }
        }
    }
}
