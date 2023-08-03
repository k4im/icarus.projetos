```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                           Method |      Mean |      Error |    StdDev | Rank |    Gen0 | Allocated |
|--------------------------------- |----------:|-----------:|----------:|-----:|--------:|----------:|
|          BuscarProdutosPaginados | 483.83 μs |  25.120 μs |  1.377 μs |    2 | 52.7344 |  81.23 KB |
|       BuscarProdutosPaginadosSql | 739.73 μs | 473.192 μs | 25.937 μs |    3 | 66.4063 | 103.47 KB |
| BuscarProdutosPaginadosSqlDapper |  59.86 μs |   6.366 μs |  0.349 μs |    1 |  3.4180 |   5.38 KB |
