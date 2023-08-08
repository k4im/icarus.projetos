namespace projeto.infra.Repository;

public interface IRepoProdutosDisponiveis
{
    Task<Response<ProdutosDisponiveis>> BuscarTodosProdutos();
    Task<ProdutosDisponiveis> BuscarPorId(int id);

    Task<bool> RemoverProdutos(int id);
    Task<bool> AdicionarProdutos(ProdutosDisponiveis model);

    Task<bool> AtualizarProdutos(int id, ProdutosDisponiveis model);
}
