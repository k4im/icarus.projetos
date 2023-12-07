using BenchmarkDotNet.Attributes;
using projeto.databasePort;
using projeto.databaseAdapters;
using Microsoft.EntityFrameworkCore;

namespace Benchmark;

[MemoryDiagnoser, SimpleJob]
public class BenchmarkEntityFrameWork
{

    [Benchmark]
    public async Task EntityframeWorkFindMethod() {
        var conexao = new DataBaseConnectionMysql();
        var db = conexao.ConnectionEntityFrameWork();
        await db.Projetos.FirstOrDefaultAsync(x => x.Id == 1);
    }

    [Benchmark]
    public async Task EntityFrameWorkWhereMethod() {
        var conexao = new DatabaseConnectionSQLITE();
        var db = conexao.ConnectionEntityFrameWork();
        await db.Projetos.FindAsync(1);
    } 
}