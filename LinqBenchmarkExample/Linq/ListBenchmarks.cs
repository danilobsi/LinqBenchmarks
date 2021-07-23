using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System.Collections.Generic;
using System.Linq;


namespace BenchmarkAndSpanExample.Linq
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [ShortRunJob]
    public class ListBenchmarks
    {
        ComplexSample[] complexSamples;

        [Params(10, 1000)]
        public int arraySize { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            complexSamples = new ComplexSample[arraySize];
            for (var i = 0; i < arraySize; i++)
                complexSamples[i] = new ComplexSample
                {
                    Id = i,
                    Name = "SayMyName",
                    Description = "More information over the sample"
                };
        }

#if DEBUG
        public ListBenchmarks()
        {
            arraySize = 1000;
            GlobalSetup();
        }
#endif

        [Benchmark(Baseline = true)]
        public void SelectToList()
        {
            _ = complexSamples.Where(s => s.Id % 2 == 1).Select(ToSample).ToList();
        }

        [Benchmark]
        public void ListToList()
        {
            var items = complexSamples.Where(s => s.Id % 2 == 1).ToList();
            var samples = new List<Sample>();
            foreach(var item in items)
            {
                //Do something else

                samples.Add(ToSample(item));
            }                
        }

        [Benchmark]
        public void EnumerableToList()
        {
            var items = complexSamples.Where(s => s.Id % 2 == 1);
            var samples = new List<Sample>();
            foreach (var item in items)
            {
                //Do something else

                samples.Add(ToSample(item));
            }
        }

        [Benchmark]
        public void ForeachToList()
        {
            var samples = new List<Sample>();
            foreach (var item in complexSamples)
            {
                if (item.Id % 2 == 1)
                {
                    //Do something else

                    samples.Add(ToSample(item));
                }
            }
        }

        public Sample ToSample(ComplexSample sample) => new Sample
        {
            Id = sample.Id,
            Name = sample.Name
        };
    }
}
