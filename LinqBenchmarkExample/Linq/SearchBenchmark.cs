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
    public class SearchBenchmark
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
        public SearchBenchmark()
        {
            arraySize = 1000;
            GlobalSetup();
        }
#endif

        [Benchmark(Baseline = true)]
        public void FirstOrDefaultSearch()
        {
            var itemFound = samples.FirstOrDefault(s => s.Id % 2 == 1);
            if (itemFound != null)
            {
                _ = new Sample { Name = itemFound.Name };
            }
        }

        [Benchmark]
        public void AnySearch()
        {
            var found = samples.Any(s => s.Id % 2 == 1);
            if (found)
            {
                var itemFound = samples.First(s => s.Id % 2 == 1);
                _ = new Sample { Name = itemFound.Name };
            }
        }

        [Benchmark]
        public void NewEnumerableFirstOrDefaultSearch()
        {
            var items = samples.Select(sample => new Sample
            {
                Id = sample.Id,
                Name = sample.Name
            });
            var itemFound = items.FirstOrDefault(s => s.Id % 2 == 1);
            if (itemFound != null)
            {
                _ = itemFound;
            }
        }

        [Benchmark]
        public void NewEnumerableAnySearch()
        {
            var items = samples.Select(sample => new Sample { 
                Id = sample.Id,
                Name = sample.Name
            });
            if (items.Any(s => s.Id % 2 == 1))
            {
                _ = items.First(s => s.Id % 2 == 1);
            }
        }

        [Benchmark]
        public void NewListExistSearch()
        {
            var items = samples.Select(sample => new Sample
            {
                Id = sample.Id,
                Name = sample.Name
            }).ToList();
            if (items.Exists(s => s.Id % 2 == 1))
            {
                _ = items.First(s => s.Id % 2 == 1);
            }
        }
    }
}
