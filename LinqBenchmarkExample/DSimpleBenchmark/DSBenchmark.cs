using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LinqBenchmarkExample.DSimpleBenchmark
{
    public static class DSBenchmark
    {
        static Dictionary<string, (int count, double totalElapsedTicks)> _averageTimePerMethod = new Dictionary<string, (int, double)>();
        static bool _started = false;

        public static void Print<T>(Func<T> functionToMeasure, int warmUpExecutions = 5000, int benchMarkExecutions = 1000)
        {
            var text = new StringBuilder();
            //Warm up
            for (var i = 0; i < warmUpExecutions; i++)
                functionToMeasure();

            double average = 0;
            _started = true;

            //Measure
            for (var i = 0; i < benchMarkExecutions; i++)
            {
                var watch = new Stopwatch();

                watch.Start();
                functionToMeasure();
                watch.Stop();

                average = ((average * i) + watch.ElapsedTicks) / (i + 1);
            }
            text.AppendLine($"{functionToMeasure.Method.Name}: {functionToMeasure()} - avg: {average} ticks");

            foreach (var item in _averageTimePerMethod)
            {
                text.AppendLine($"    {item.Key}: {item.Value.count} calls - avg: {item.Value.totalElapsedTicks / item.Value.count} ticks");
            }

            Console.Write(text.ToString());
        }

        public static T TraceMethod<T>(Func<T> action)
        {
            if (!_started)
            {
                return action();
            }

            var key = action.Method.Name;

            var watch = Stopwatch.StartNew();
            try
            {
                return action();
            }
            finally
            {
                watch.Stop();
                if (_averageTimePerMethod.TryGetValue(key, out var ticks))
                {
                    _averageTimePerMethod[key] = (ticks.count + 1, ticks.totalElapsedTicks + watch.ElapsedTicks);
                }
                else
                {
                    _averageTimePerMethod.Add(key, (1, watch.ElapsedTicks));
                }
            }
        }
    }
}
