namespace projeto.infra.Repository;

public interface IRepoProdutosDisponiveis
{
    Task<List<ProdutoEmEstoqueDTO>> BuscarTodosProdutos();
    Task<bool> RemoverProdutos(int id);
    Task<bool> AdicionarProdutos(ProdutosDisponiveis model);

    Task<bool> AtualizarProdutos(int id, ProdutosDisponiveis model);
}
