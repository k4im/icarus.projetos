```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method | numerosDeInteracao |       Mean |       Error |   StdDev | Rank |     Gen0 | Allocated |
|--------------------------- |------------------- |-----------:|------------:|---------:|-----:|---------:|----------:|
|    **buscarProdutosPaginados** |               **1000** | **1,749.1 μs** |   **242.99 μs** | **13.32 μs** |    **3** | **205.0781** | **316.53 KB** |
| buscarProdutosPaginadosSql |               1000 |   127.5 μs |    80.81 μs |  4.43 μs |    2 |  10.7422 |  16.48 KB |
|    **buscarProdutosPaginados** |               **2000** | **3,388.9 μs** | **1,131.21 μs** | **62.01 μs** |    **4** | **410.1563** | **629.43 KB** |
| buscarProdutosPaginadosSql |               2000 |   122.4 μs |    21.86 μs |  1.20 μs |    1 |  10.7422 |  16.48 KB |
