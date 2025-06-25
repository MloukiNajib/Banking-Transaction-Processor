```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.5965/22H2/2022Update)
Intel Core i7-6820HQ CPU 2.70GHz (Max: 2.71GHz) (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method        | Mean      | Error    | StdDev   | Median    | Ratio | RatioSD | Gen0    | Gen1    | Gen2   | Allocated | Alloc Ratio |
|-------------- |----------:|---------:|---------:|----------:|------:|--------:|--------:|--------:|-------:|----------:|------------:|
| &#39;Small batch&#39; |  23.24 μs | 1.021 μs | 2.979 μs |  21.47 μs |  1.01 |    0.17 |  6.2866 |  2.0752 |      - |  24.23 KB |        1.00 |
| &#39;Large batch&#39; | 329.41 μs | 6.550 μs | 7.008 μs | 326.33 μs | 14.39 |    1.67 | 91.7969 | 90.8203 | 1.9531 | 523.62 KB |       21.61 |
