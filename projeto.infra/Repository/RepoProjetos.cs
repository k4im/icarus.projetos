namespace projeto.infra.Repository;
public class RepoProjetos : IRepoProjetos
{
    //Delegates e Eventos a serem disparados
    public delegate void aoCriarProjetoEventHandler(Projeto model);
    public event aoCriarProjetoEventHandler AocriarProjeto;
    readonly IMessageBusService _messageBroker;
    readonly IDataBaseConnection _dataConnection;
    public RepoProjetos(IMessageBusService messageBroker, IDataBaseConnection dataConnection = null)
    {
        _messageBroker = messageBroker;
        AocriarProjeto += async (Projeto model) => { await RepoProdutosDisponiveis.AtualizarTabelaProdutosDisponiveis(model); };
        AocriarProjeto += _messageBroker.EnviarProjeto;
        _dataConnection = dataConnection;
    }

    public async Task<bool> AtualizarStatus(string model, int? id)
    {
        try
        {
            using var db = _dataConnection.ConnectionEntityFrameWorkMysqlProduction();
            var projeto = await db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
            projeto.AtualizarStatus(model);
            await db.SaveChangesAsync();
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

    public async Task<ProjetoBuscaIdDTO> BuscarPorId(int? id)
    {
        using var connection = _dataConnection.ConnectionForDapperMysqlProduction();
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
            var result = await connection.QueryAsync<ProjetoBuscaIdDTO, ProdutoEmEstoqueDTO, ProjetoBuscaIdDTO>(query,
            map: (p, c) =>
            {
                p.ProdutoUtilizado = c;
                return p;
            },
            param: new { @busca = id });
            return result.FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Response<ProjetoPaginadoDTO>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        var queryPaginado = "SELECT Id, Nome, Status, DataInicio, DataEntrega, Valor FROM Projetos LIMIT @resultado OFFSET @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos";

        using var connection = _dataConnection.ConnectionForDapperMysqlProduction();
        var totalItems = await connection.ExecuteScalarAsync<int>(queryTotal);
        var total = Math.Ceiling(totalItems / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total, totalItems);
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {
        try
        {
            using var db = _dataConnection.ConnectionEntityFrameWorkMysqlProduction();
            db.Projetos.Add(model);
            await db.SaveChangesAsync();
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

    public async Task<bool> DeletarProjeto(int? id)
    {
        try
        {
            using var db = _dataConnection.ConnectionEntityFrameWorkMysqlProduction();
            await db.Projetos.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
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

        using var connection = _dataConnection.ConnectionForDapperMysqlProduction();
        var totalItems = await connection.ExecuteScalarAsync<int>(queryTotal, new {filter = filtro});
        var total = Math.Ceiling(totalItems / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina, filter = $"%{filtro}%" });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total, totalItems);
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

        using var connection = _dataConnection.ConnectionForDapperMysqlProduction();
        var totalItems = await connection.ExecuteScalarAsync<int>(queryTotal, new {filter = filtro});
        var total = Math.Ceiling(totalItems / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina, filter = filtro });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total, totalItems);
    }
}
