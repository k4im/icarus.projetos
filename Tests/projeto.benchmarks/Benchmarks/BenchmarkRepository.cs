namespace projeto.benchmarks.Benchmarks;

[MemoryDiagnoser, RankColumn, ShortRunJob]
public class BenchmarkRepository
{
    DataContext _context;
    [Params(10, 150, 2000)]
    public int numerosDeInteracao;

    [GlobalSetup]
    public void Setup()
    {
        _context = new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Data").Options);
        for (int i = 0; i < numerosDeInteracao; i++)
            _context.Projetos.AddRange(FakeProjeto.factoryListaProjetos());
    }

    [Benchmark]
    public async Task<Response<Projeto>> buscarProdutos()
    {
        int pagina = 1;
        double resultadoPorPagina = 5;
        var ResultadoPorPagina = resultadoPorPagina;
        var projetos = await _context.Projetos.ToListAsync();
        var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);
        var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
        return new Response<Projeto>(projetosPaginados, pagina, (int)TotalDePaginas);
    }

    [Benchmark]
    public async Task atualizarProdutosPadrao()
    {
        _context.Projetos.Add(FakeProjeto.factoryProjeto());
        await _context.SaveChangesAsync();
        var produto = await _context.Projetos.FirstOrDefaultAsync(x => x.Id == 1);
        produto.AtualizarStatus(new StatusProjeto("Teste"));
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async void atualizarProdutosOtimizado()
    {
        _context.Projetos.Add(FakeProjeto.factoryProjeto());
        await _context.SaveChangesAsync();
        _context.Projetos.Where(x => x.Id == 1)
        .ExecuteUpdate(setter => setter.SetProperty(projeto => projeto.Status, new StatusProjeto("teste")));
    }

}
