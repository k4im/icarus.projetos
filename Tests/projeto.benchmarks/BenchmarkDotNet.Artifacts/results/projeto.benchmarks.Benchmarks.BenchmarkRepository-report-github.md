```

BenchmarkDotNet v0.13.6, Linux Mint 21.1 (Vera)
Intel Core i3-3240 CPU 3.40GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.109
  [Host]   : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX
  ShortRun : .NET 7.0.9 (7.0.923.32301), X64 RyuJIT AVX

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method | numerosDeInteracao | Mean | Error | Rank |
|--------------------------- |------------------- |-----:|------:|-----:|
|    **buscarProdutosPaginados** |               **1000** |   **NA** |    **NA** |    **?** |
| buscarProdutosPaginadosSql |               1000 |   NA |    NA |    ? |
|    **buscarProdutosPaginados** |               **2000** |   **NA** |    **NA** |    **?** |
| buscarProdutosPaginadosSql |               2000 |   NA |    NA |    ? |

Benchmarks with issues:
  BenchmarkRepository.buscarProdutosPaginados: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [numerosDeInteracao=1000]
  BenchmarkRepository.buscarProdutosPaginadosSql: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [numerosDeInteracao=1000]
  BenchmarkRepository.buscarProdutosPaginados: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [numerosDeInteracao=2000]
  BenchmarkRepository.buscarProdutosPaginadosSql: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [numerosDeInteracao=2000]
