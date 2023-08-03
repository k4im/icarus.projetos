using Dapper;
using Microsoft.Data.Sqlite;

namespace projeto.benchmarks.Benchmarks;

[MemoryDiagnoser, RankColumn, ShortRunJob]
public class BenchmarkRepository
{
    [Params(1_000, 2_000)]
    public int Interations;

    // [GlobalSetup]
    // public async void Setup()
    // {
    //     var connection = new SqliteConnection("Data Source=:memory:");
    //     await connection.OpenAsync();
    //     _context = new DataContext(new DbContextOptionsBuilder().UseSqlite(connection).Options);
    //     _context.Database.EnsureCreated();
    //     for (int i = 0; i < numerosDeInteracao; i++)
    //         _context.Projetos.AddRange(FakeProjeto.factoryProjeto());
    //     await _context.SaveChangesAsync();
    // }

    [Benchmark]
    public async Task<Response<Projeto>> BuscarProdutosPaginados()
    {
        using (var db = new DataContext())
        {
            var pessoas = await db.Projetos.ToListAsync();
            var totalDePaginas = Math.Ceiling(pessoas.Count() / 10f);
            var produtosPaginados = pessoas.Skip((1 - 1) * (int)10).Take((int)10).ToList();
            var paginasTotal = (int)totalDePaginas;
            return new Response<Projeto>(produtosPaginados, 1, (int)totalDePaginas);
        };
    }


    [Benchmark]
    public async Task<Response<Projeto>> BuscarProdutosPaginadosSql()
    {
        using (var context = new DataContext())
        {
            var total = await context.Projetos.FromSql($"SELECT COUNT(*) FROM Projetos").CountAsync();
            var projetosPaginados = await context.Projetos.FromSql($"SELECT * FROM Projetos LIMIT 10 OFFSET 0").ToListAsync();
            return new Response<Projeto>(projetosPaginados, 1, total);
        }
    }

    [Benchmark]
    public async Task<Response<Projeto>> BuscarProdutosPaginadosSqlDapper()
    {
        using (var context = new SqliteConnection("Data Source=teste.db;"))
        {
            var total = context.Query($"SELECT COUNT(*) FROM Projetos").Count();
            var query = $"SELECT * FROM Projetos LIMIT 10 OFFSET 0";
            var projetos = await context.QueryAsync<Projeto>(query);
            return new Response<Projeto>(projetos.ToList(), 1, total);
        }
    }
}
