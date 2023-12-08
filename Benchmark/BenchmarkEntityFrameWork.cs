namespace Benchmark;
[MemoryDiagnoser, SimpleJob]
public class BenchmarkEntityFrameWork
{

    [Benchmark]
    public async Task EntityframeWorkFindMethod() {
        var conexao = new DatabaseConnectionInMemory();
        var db = conexao.ConnectionEntityFrameWork();
        await db.Projetos.FirstOrDefaultAsync(x => x.Id == 1);
    }

    [Benchmark]
    public async Task EntityFrameWorkWhereMethod() {
        var conexao = new DatabaseConnectionInMemory();
        var db = conexao.ConnectionEntityFrameWork();
        await db.Projetos.FindAsync(1);
    }
}