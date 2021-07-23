# Linq Benchmark Example

A basic benchmarks for Linq

## Linq Performance Results

|                  Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |----------:|----------:|----------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
|            ForeachCount |  1.134 us | 0.0219 us | 0.0194 us |  0.11 |    0.00 |    1 |           - |           - |           - |                   - |
|     LinqEnumerableCount |  3.646 us | 0.0720 us | 0.0674 us |  0.36 |    0.02 |    2 |      0.0114 |           - |           - |                48 B |
|           LinqListCount |  7.973 us | 0.1593 us | 0.2481 us |  0.80 |    0.04 |    3 |      2.6779 |           - |           - |              8440 B |
|               LinqCount | 10.027 us | 0.1959 us | 0.2932 us |  1.00 |    0.00 |    5 |           - |           - |           - |                32 B |
|      YieldFilterForeach |  8.291 us | 0.1653 us | 0.2091 us |  0.83 |    0.03 |    4 |      5.0964 |           - |           - |             16056 B |
| EnumerableFilterForeach | 10.097 us | 0.1980 us | 0.3024 us |  1.01 |    0.04 |    5 |      5.0964 |           - |           - |             16048 B |
|    ForeachFilterForeach | 13.109 us | 0.2593 us | 0.5355 us |  1.31 |    0.07 |    6 |      7.7515 |           - |           - |             24432 B |
|       ListFilterForeach | 13.481 us | 0.2643 us | 0.4489 us |  1.35 |    0.06 |    7 |      7.7515 |           - |           - |             24440 B |
|        ListForeachTwice | 19.247 us | 0.3777 us | 0.7095 us |  1.92 |    0.10 |    8 |     12.8479 |           - |           - |             40440 B |
|  EnumerableForeachTwice | 19.662 us | 0.3905 us | 0.8893 us |  1.95 |    0.08 |    8 |     10.1929 |           - |           - |             32096 B |


  
|           Method | arraySize |        Mean |       Error |      StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------- |---------- |------------:|------------:|------------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
|    ForeachToList |        10 |    127.8 ns |    12.97 ns |   0.7108 ns |  0.40 |    0.01 |    1 |      0.1092 |           - |           - |               344 B |
| EnumerableToList |        10 |    253.0 ns |   121.29 ns |   6.6481 ns |  0.80 |    0.03 |    2 |      0.1245 |           - |           - |               392 B |
|     SelectToList |        10 |    317.0 ns |    76.21 ns |   4.1774 ns |  1.00 |    0.00 |    3 |      0.1626 |           - |           - |               512 B |
|       ListToList |        10 |    354.4 ns |    62.13 ns |   3.4054 ns |  1.12 |    0.00 |    4 |      0.1826 |           - |           - |               576 B |
|    ForeachToList |      1000 |  7,114.8 ns |   540.21 ns |  29.6109 ns |  0.63 |    0.01 |    1 |      7.7515 |           - |           - |             24392 B |
|     SelectToList |      1000 | 11,207.1 ns | 2,414.70 ns | 132.3581 ns |  1.00 |    0.00 |    2 |      7.7972 |           - |           - |             24560 B |
| EnumerableToList |      1000 | 12,179.8 ns | 1,343.24 ns |  73.6278 ns |  1.09 |    0.02 |    3 |      7.7515 |           - |           - |             24440 B |
|       ListToList |      1000 | 15,223.2 ns | 2,364.99 ns | 129.6332 ns |  1.36 |    0.01 |    4 |     10.4218 |           - |           - |             32832 B |

## Conclusion
- 	Sometimes things don’t work as expected
- 	For performance, manual implementation is the best choice (Risk of compromising readability)
- 	Lists are costly
- 	IEnumerables are the best cost-effective solution for most cases