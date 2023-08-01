namespace projeto.benchmarks.Benchmarks;

[MemoryDiagnoser, RankColumn, ShortRunJob]
public class BenchmarkRepository
{
    RepoProjetos _repo;
    [Params(10, 150, 250)]
    public int numerosDeInteracao;
    [GlobalSetup]
    public async void Setup()
    {
        _repo = new RepoProjetos(new Mock<IMessageBusService>().Object);
        var repoProduto = new RepoProdutosDisponiveis();
        await repoProduto.adicionarProdutos(FakeProduto.factoryProdutos());
        for (int i = 0; i < numerosDeInteracao; i++)
            await _repo.CriarProjeto(FakeProjeto.factoryProjeto());
    }

    [Benchmark]
    public async Task<Response<Projeto>> buscarProdutos()
        => await _repo.BuscarProdutos(1, 5);


}
