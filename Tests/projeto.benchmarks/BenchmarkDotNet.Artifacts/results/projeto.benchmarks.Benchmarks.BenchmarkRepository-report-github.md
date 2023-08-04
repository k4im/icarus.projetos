```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]  : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  LongRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=LongRun  IterationCount=100  LaunchCount=3  
WarmupCount=15  

```
|                           Method | interation |      Mean |    Error |   StdDev |    Median | Rank |    Gen0 | Allocated |
|--------------------------------- |----------- |----------:|---------:|---------:|----------:|-----:|--------:|----------:|
|          **BuscarProdutosPaginados** |       **1000** | **442.37 μs** | **1.181 μs** | **5.854 μs** | **441.06 μs** |    **3** | **52.7344** |  **81.23 KB** |
|       BuscarProdutosPaginadosSql |       1000 | 585.17 μs | 1.702 μs | 8.514 μs | 583.53 μs |    4 | 64.4531 |  99.89 KB |
| BuscarProdutosPaginadosSqlDapper |       1000 |  54.75 μs | 0.100 μs | 0.499 μs |  54.76 μs |    1 |  3.1128 |   4.82 KB |
|          **BuscarProdutosPaginados** |       **2000** | **438.47 μs** | **0.871 μs** | **4.408 μs** | **437.74 μs** |    **3** | **52.7344** |  **81.23 KB** |
|       BuscarProdutosPaginadosSql |       2000 | 587.20 μs | 1.157 μs | 5.703 μs | 587.47 μs |    4 | 64.4531 |  99.89 KB |
| BuscarProdutosPaginadosSqlDapper |       2000 |  57.84 μs | 0.574 μs | 2.871 μs |  57.05 μs |    2 |  3.1128 |   4.82 KB |
