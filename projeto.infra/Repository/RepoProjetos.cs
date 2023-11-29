namespace projeto.infra.Repository;
public class RepoProjetos : IRepoProjetos
{
    //Delegates e Eventos a serem disparados
    public delegate void aoCriarProjetoEventHandler(Projeto model);
    public event aoCriarProjetoEventHandler AocriarProjeto;
    readonly IMessageBusService _messageBroker;
    readonly IDatabaseAdapterProjeto _dataConnection;
    public RepoProjetos(IMessageBusService messageBroker, IDatabaseAdapterProjeto dataConnection)
    {
        _messageBroker = messageBroker;
        _dataConnection = dataConnection;
        AocriarProjeto += async (Projeto model) => { await RepoProdutosDisponiveis.AtualizarTabelaProdutosDisponiveis(model); };
        AocriarProjeto += _messageBroker.EnviarProjeto;
    }

    public async Task<bool> AtualizarStatus(string model, int id)
    {
        try
        {
            await _dataConnection.AtualizarStatusEmBanco(model, id);
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel estar realizando a operação, a mesma já foi realizada por um outro usuario!");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel estar realizando a operação: {e.Message}");
            return false;
        }
    }

    public async Task<ProjetoBuscaIdDTO> BuscarPorId(int id)
    {
        try
        {
            var query = @"
            SELECT 
                Projetos.*,
                ProdutosEmEstoque.*
            FROM 
                Projetos 
            INNER JOIN 
                ProdutosEmEstoque
            ON 
                Projetos.ProdutoUtilizadoId = ProdutosEmEstoque.Id
            WHERE 
                Projetos.Id 
            LIKE 
                @busca";
            return await _dataConnection.BuscarDadosDoBancoPeloId(id, query);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Response<ProjetoPaginadoDTO>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        var queryPaginado = @"
        SELECT 
            Id, 
            Nome, 
            Status, 
            DataInicio, 
            DataEntrega, 
            Valor 
        FROM 
            Projetos 
        LIMIT 
            @resultado 
        OFFSET 
            @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos";
        return await _dataConnection.PaginarProjetosDoBancoDapper(
            queryPaginado, queryTotal, 
            pagina, resultadoPorPagina);
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {
        try
        {
            await _dataConnection.SalvarProjetoEmBancoEntityFrameWork(model);
            AocriarProjeto(model);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel realizar a operação, a mesma já foi realizado por um outro usuario!");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação: {e.Message}");
            return false;
        }
    }

    public async Task<bool> DeletarProjeto(int id)
    {
        try
        {
            await _dataConnection.DeletarProjetoDoBancoEntityFrameWork(id);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel realizar a operação, a mesma já foi realizado por um outro usuario!");
            return false;
        }
        catch (Exception e)
        {
            Console.Write($"Não foi possivel realizar a operação: {e.Message}");
            return false;
        }
    }

    public async Task<Response<ProjetoPaginadoDTO>> BuscarProdutosFiltrados(int pagina, float resultadoPorPagina, string filtro)
    {
        var queryPaginado = @"
        SELECT 
            Id, 
            Nome, 
            Status, 
            DataInicio, 
            DataEntrega, 
            Valor 
        FROM 
            Projetos 
        WHERE
            Nome
        LIKE
            @filter         
        LIMIT 
            @resultado 
        OFFSET 
            @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos WHERE Status LIKE @filter OR Nome LIKE @filter";
        return await _dataConnection.FiltrarDadosPaginadosDoBancoDapper(
        queryPaginado, filtro, 
        queryTotal, pagina, 
        resultadoPorPagina);

    }

    public async Task<Response<ProjetoPaginadoDTO>> FiltrarPorStatus(int pagina, float resultadoPorPagina, string filtro)
    {
        var queryPaginado = @"
        SELECT 
            Id, 
            Nome, 
            Status, 
            DataInicio, 
            DataEntrega, 
            Valor 
        FROM 
            Projetos 
        WHERE
            Status 
        LIKE
            @filter   
        LIMIT 
            @resultado 
        OFFSET 
            @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos WHERE Status LIKE @filter OR Nome LIKE @filter";
        return await _dataConnection.FiltrarDadosPaginadosDoBancoDapper(
        queryPaginado, 
        filtro, queryTotal, 
        pagina, resultadoPorPagina);

    }
}
