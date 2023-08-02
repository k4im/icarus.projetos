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
        _context = new DataContext(new DbContextOptionsBuilder().UseSqlite("Data Source=mydb.db").Options);
        _context.Database.Migrate();
        for (int i = 0; i < numerosDeInteracao; i++)
            _context.Projetos.AddRange(FakeProjeto.factoryProjeto());
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task<Response<Projeto>> buscarProdutosPaginados()
    {
        int pagina = 1;
        var ResultadoPorPagina = 5f;
        var projetos = await _context.Projetos.ToListAsync();
        var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);

        var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
        return new Response<Projeto>(projetosPaginados, pagina, (int)TotalDePaginas);
    }

    [Benchmark]
    public async Task<List<Projeto>> buscarProdutosPaginadosSql()
    => await _context.Projetos.FromSql($"SELECT * FROM Data.Projetos LIMIT 5,10")
    .ToListAsync();
}
