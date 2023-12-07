namespace projeto.databaseAdapters;
public class DatabaseAdapter : IDatabaseAdapter
{
    IDataBaseConnection _dataConnection;
    public DatabaseAdapter()
        => _dataConnection = new DataBaseConnectionMysql();

    public DatabaseAdapter(IDataBaseConnection dataConnection)
    {
        _dataConnection = dataConnection;
    }

    public async Task AtualizarBancoDeDadosDeProdutos(Projeto model)
    {
        using var db = _dataConnection.ConnectionEntityFrameWork();
        var produto = await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizadoId);
        produto.Quantidade -= model.QuantidadeUtilizado;
        if(produto.Quantidade <= 0) {
            db.Remove(produto);
            
        }
        await db.SaveChangesAsync();
    }


    public async Task AtualizarStatusEmBanco(Projeto model, int id)
    {
        using var db = _dataConnection.ConnectionEntityFrameWork();
        var projeto = await db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
        projeto.AtualizarObjeto(model);
        await db.SaveChangesAsync();
    }

    public async Task<ProjetoBuscaIdDTO> BuscarDadosDoBancoPeloId(int id, string query)
    {
        using var connection = _dataConnection.ConnectionForDapper();
        var result = await connection.QueryAsync<ProjetoBuscaIdDTO, ProdutoEmEstoqueDTO, 
        ProjetoBuscaIdDTO>(query,
        map: (p, c) =>
        {
            p.ProdutoUtilizado = c;
            return p;
        },
        param: new { @busca = id });
        return result.FirstOrDefault();    
    }

    public async Task<bool> DeletarProjetoDoBancoEntityFrameWork(int id)
    {
        try 
        {
            using var db = _dataConnection.ConnectionEntityFrameWork();
            await db.Projetos.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
            return true;
        }        
        catch (Exception ex) 
        {
            return false;
        }
    }

    public async Task<Response<ProjetoPaginadoDTO>> FiltrarDadosPaginadosDoBancoDapper(
        string queryPaginacao, 
        string filtro, 
        string queryTotalDeItens, 
        int pagina, 
        float resultadoPorPagina)
    {
        using var connection = _dataConnection.ConnectionForDapper();
        var totalItems = await connection.ExecuteScalarAsync<int>(queryTotalDeItens, new {filter = filtro});
        var total = Math.Ceiling(totalItems / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginacao, new { resultado = resultadoPorPagina, 
            pagina = (pagina - 1) * resultadoPorPagina, filter = $"%{filtro}%" });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total, totalItems);
    }

    public async Task<Response<ProjetoPaginadoDTO>> PaginarProjetosDoBancoDapper(string queryPaginacao, 
    string queryTotalDeItens, 
    int pagina, 
    float resultadoPorPagina)
    {
        using var connection = _dataConnection.ConnectionForDapper();
        var totalItems = await connection.ExecuteScalarAsync<int>(queryTotalDeItens);
        var total = Math.Ceiling(totalItems / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginacao, new { resultado = resultadoPorPagina, 
            pagina = (pagina - 1) * resultadoPorPagina });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total, totalItems);
    }

    public async Task<bool> SalvarProjetoEmBancoEntityFrameWork(Projeto model)
    {
        try 
        {
            using var db = _dataConnection.ConnectionEntityFrameWork();
            db.Projetos.Add(model);
            await db.SaveChangesAsync();
            return true;
        }
        catch(Exception ex) {
            return false;
        }
        
    }
}
