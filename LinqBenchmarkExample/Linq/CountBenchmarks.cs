using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System.Linq;

namespace BenchmarkAndSpanExample.Linq
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    //[SimpleJob(RunStrategy.Monitoring, launchCount: 3, warmupCount: 5, targetCount: 20)]
    [ShortRunJob]
    public class CountBenchmarks
    {
        Sample[] samples;

        [Params(10, 1000)]
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
        public CountBenchmarks()
        {
            arraySize = 1000;
            GlobalSetup();
        }
#endif

        [Benchmark(Baseline = true)]
        public int LinqListCount()
        {
            var items = samples.Where(s => s.Id % 2 == 1).ToList();
            
            return items.Count;
        }

        [Benchmark]
        public int LinqEnumerableCount()
        {
            var items = samples.Where(s => s.Id % 2 == 1);

            return items.Count();
        }

        [Benchmark]
        public int LinqCount()
        {
            return samples.Count(s => s.Id % 2 == 1);
        }

        [Benchmark]
        public int ForeachCount()
        {
            int count = 0;
            foreach (var sample in samples)
            {
                if (sample.Id % 2 == 1)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
