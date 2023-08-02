```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method | numerosDeInteracao |       Mean |     Error |   StdDev | Rank |     Gen0 | Allocated |
|--------------------------- |------------------- |-----------:|----------:|---------:|-----:|---------:|----------:|
|    **buscarProdutosPaginados** |               **1000** | **1,960.9 μs** | **163.11 μs** |  **8.94 μs** |    **3** | **205.0781** | **316.53 KB** |
| buscarProdutosPaginadosSql |               1000 |   121.5 μs |   9.82 μs |  0.54 μs |    2 |  10.7422 |  16.51 KB |
|    **buscarProdutosPaginados** |               **2000** | **3,332.4 μs** | **692.43 μs** | **37.95 μs** |    **4** | **410.1563** | **629.43 KB** |
| buscarProdutosPaginadosSql |               2000 |   118.8 μs |  25.10 μs |  1.38 μs |    1 |  10.7422 |  16.51 KB |
