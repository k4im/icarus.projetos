using System.Text.RegularExpressions;

namespace projeto.benchmarks.Benchmarks;
[MemoryDiagnoser, ShortRunJob, RankColumn]
public class BenchMarkValidation
{

    [Params(100, 250)]
    public int Iteration;

    [Benchmark]
    public bool RegexSemCompilar()
        => !Regex.IsMatch("15", @"^[a-zA-Z ]+$");

    [Benchmark]
    public bool RegexCompilado()
     => !Regex.IsMatch("15", @"^[a-zA-Z ]+$", RegexOptions.Compiled);
}
