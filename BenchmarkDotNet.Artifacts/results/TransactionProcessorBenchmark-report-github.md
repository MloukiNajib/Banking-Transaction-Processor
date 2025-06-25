```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.5965/22H2/2022Update)
Intel Core i7-6820HQ CPU 2.70GHz (Max: 2.71GHz) (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.301
  [Host]   : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.6 (9.0.625.26613), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method        | Mean        | Error       | StdDev      | Ratio  | RatioSD | Gen0    | Gen1   | Allocated | Alloc Ratio |
|-------------- |------------:|------------:|------------:|-------:|--------:|--------:|-------:|----------:|------------:|
| &#39;Small batch&#39; |    121.6 ns |     1.48 ns |     1.15 ns |   1.00 |    0.01 |  0.0899 |      - |     376 B |        1.00 |
| &#39;Large batch&#39; | 86,121.5 ns | 1,593.54 ns | 2,662.44 ns | 708.25 |   22.54 | 34.0576 | 4.8828 |  142904 B |      380.06 |
