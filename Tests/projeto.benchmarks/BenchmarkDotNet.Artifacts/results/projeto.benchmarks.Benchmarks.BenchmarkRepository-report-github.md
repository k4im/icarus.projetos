```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method | numerosDeInteracao |     Mean |     Error |   StdDev | Rank |   Gen0 | Allocated |
|--------------- |------------------- |---------:|----------:|---------:|-----:|-------:|----------:|
| **buscarProdutos** |                 **10** | **13.27 μs** | **21.680 μs** | **1.188 μs** |    **3** | **2.3956** |   **3.68 KB** |
| **buscarProdutos** |                **150** | **11.79 μs** |  **0.428 μs** | **0.023 μs** |    **1** | **2.3956** |   **3.68 KB** |
| **buscarProdutos** |               **2000** | **12.85 μs** |  **3.444 μs** | **0.189 μs** |    **2** | **2.3956** |   **3.68 KB** |
