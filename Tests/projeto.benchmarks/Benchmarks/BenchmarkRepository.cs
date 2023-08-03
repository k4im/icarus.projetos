using Microsoft.Data.Sqlite;

namespace projeto.benchmarks.Benchmarks;

[MemoryDiagnoser, RankColumn, ShortRunJob]
public class BenchmarkRepository
{
    DataContext _context;
    [Params(1_000, 2_000)]
    public int numerosDeInteracao;

    [GlobalSetup]
    public async void Setup()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        _context = new DataContext(new DbContextOptionsBuilder().UseSqlite(connection).Options);
        _context.Database.EnsureCreated();
        for (int i = 0; i < numerosDeInteracao; i++)
            _context.Projetos.AddRange(FakeProjeto.factoryProjeto());
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task<Response<Projeto>> BuscarProdutosPaginados()
    {
        int pagina = 1;
        var ResultadoPorPagina = 5f;
        var projetos = await _context.Projetos.ToListAsync();
        var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);

        var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
        return new Response<Projeto>(projetosPaginados, pagina, (int)TotalDePaginas);
    }

    // [Benchmark]
    // public async Task<List<Projeto>> buscarProdutosPaginadosSql()
    // => await _context.Projetos.FromSql($"SELECT * FROM Projetos LIMIT 10 OFFSET 0")
    // .ToListAsync();

    [Benchmark]
    public async Task<Response<Projeto>> BuscarProdutosPaginadosSql()
    {
        var total = await _context.Projetos.FromSql($"SELECT COUNT(*) FROM Projetos").CountAsync();
        var projetosPaginados = await _context.Projetos.FromSql($"SELECT * FROM Projetos LIMIT 10 OFFSET 0").ToListAsync();
        return new Response<Projeto>(projetosPaginados, 1, total);
    }
}
