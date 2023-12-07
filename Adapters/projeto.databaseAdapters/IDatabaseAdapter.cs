namespace projeto.databaseAdapters;
public interface IDatabaseAdapter
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

    Task AtualizarStatusEmBanco(Projeto model, int id);

    Task AtualizarBancoDeDadosDeProdutos(Projeto model);
}
