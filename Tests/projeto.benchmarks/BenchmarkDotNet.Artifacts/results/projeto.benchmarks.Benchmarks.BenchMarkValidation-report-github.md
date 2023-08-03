```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|           Method | Iteration |      Mean |      Error |   StdDev | Rank | Allocated |
|----------------- |---------- |----------:|-----------:|---------:|-----:|----------:|
| **RegexSemCompilar** |       **100** | **142.62 ns** |   **7.531 ns** | **0.413 ns** |    **3** |         **-** |
|   RegexCompilado |       100 |  79.13 ns | 117.142 ns | 6.421 ns |    2 |         - |
| **RegexSemCompilar** |       **250** | **144.76 ns** |  **35.288 ns** | **1.934 ns** |    **4** |         **-** |
|   RegexCompilado |       250 |  74.53 ns |  15.132 ns | 0.829 ns |    1 |         - |
