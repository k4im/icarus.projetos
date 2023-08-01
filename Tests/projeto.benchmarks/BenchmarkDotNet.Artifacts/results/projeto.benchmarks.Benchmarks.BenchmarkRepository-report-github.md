```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host] : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method | numerosDeInteracao | Mean | Error | Rank |
|--------------- |------------------- |-----:|------:|-----:|
| buscarProdutos |                 10 |   NA |    NA |    ? |

Benchmarks with issues:
  BenchmarkRepository.buscarProdutos: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [numerosDeInteracao=10]
