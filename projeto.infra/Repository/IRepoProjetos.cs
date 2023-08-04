namespace projeto.infra.Repository;
public interface IRepoProjetos
{
    Task<Response<ProjetoPaginadoDTO>> BuscarProdutos(int pagina, float resultadoPorPagina);
    Task<bool> CriarProjeto(Projeto model);
    Task<bool> AtualizarStatus(string model, int? id);
    Task<bool> DeletarProjeto(int? id);
    Task<Projeto> BuscarPorId(int? id);
}