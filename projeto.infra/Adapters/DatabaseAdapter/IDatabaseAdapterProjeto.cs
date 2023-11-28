namespace projeto.infra.Adapters.DatabaseAdapter;
public interface IDatabaseAdapterProjeto
{
    Task<Response<ProjetoPaginadoDTO>> PaginarProjetosDoBancoDapper(
        string queryPaginacao,
        string queryTotalDeItens,
        int pagina, 
        float resultadoPorPagina);
    Task<bool> SalvarProjetoEmBancoEntityFrameWork(Projeto model);

    Task<Response<ProjetoPaginadoDTO>> FiltrarDadosPaginadosDoBancoDapper(
        string queryPaginacao,
        string filtro,
        string queryTotalDeItens,
        int pagina, 
        float resultadoPorPagina);
    
    Task<bool> DeletarProjetoDoBancoEntityFrameWork(int id);

    Task<ProjetoBuscaIdDTO> BuscarDadosDoBancoPeloId(int id, string query);

    Task AtualizarStatusEmBanco(string status, int id);


}
