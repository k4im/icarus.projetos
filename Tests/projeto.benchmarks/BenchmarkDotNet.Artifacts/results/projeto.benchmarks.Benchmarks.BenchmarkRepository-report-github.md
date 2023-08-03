```

BenchmarkDotNet v0.13.6, Windows 10 (10.0.19045.3208/22H2/2022Update)
AMD Ryzen 5 4600G with Radeon Graphics, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.306
  [Host]   : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
  ShortRun : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method | numerosDeInteracao |        Mean |      Error |    StdDev | Rank |     Gen0 | Allocated |
|--------------------------- |------------------- |------------:|-----------:|----------:|-----:|---------:|----------:|
|    **BuscarProdutosPaginados** |               **1000** |   **714.20 μs** |  **63.348 μs** |  **3.472 μs** |    **3** | **154.2969** | **316.53 KB** |
| BuscarProdutosPaginadosSql |               1000 |    93.82 μs |   5.697 μs |  0.312 μs |    1 |  12.5732 |  25.75 KB |
|    **BuscarProdutosPaginados** |               **2000** | **1,525.46 μs** | **754.915 μs** | **41.379 μs** |    **4** | **306.6406** | **629.43 KB** |
| BuscarProdutosPaginadosSql |               2000 |   101.93 μs |   1.626 μs |  0.089 μs |    2 |  12.5732 |  25.75 KB |
