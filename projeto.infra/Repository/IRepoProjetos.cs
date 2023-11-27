namespace projeto.infra.Repository;
public interface IRepoProjetos
{
    Task<Response<ProjetoPaginadoDTO>> BuscarProdutos(int pagina, float resultadoPorPagina);
    Task<Response<ProjetoPaginadoDTO>> BuscarProdutosFiltrados(int pagina, 
    float resultadoPorPagina, 
    string filtro);
    Task<Response<ProjetoPaginadoDTO>> FiltrarPorStatus(int pagina, 
    float resultadoPorPagina, 
    string filtro);
    Task<bool> CriarProjeto(Projeto model);
    Task<bool> AtualizarStatus(string model, int id);
    Task<bool> DeletarProjeto(int id);
    Task<ProjetoBuscaIdDTO> BuscarPorId(int id);
}
