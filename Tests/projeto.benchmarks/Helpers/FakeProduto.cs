namespace projeto.benchmarks.Helpers
{
    public class FakeProduto
    {
        public static ProdutosDisponiveis factoryProdutos()
            => new ProdutosDisponiveis() { Id = 1, Nome = "Produto fake", Quantidade = 30 };
    }
}