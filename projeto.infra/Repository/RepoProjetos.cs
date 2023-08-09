
namespace projeto.infra.Repository;
public class RepoProjetos : IRepoProjetos
{
    //Delegates e Eventos a serem disparados
    public delegate void aoCriarProjetoEventHandler(Projeto model);
    public event aoCriarProjetoEventHandler AocriarProjeto;
    readonly IMessageBusService _messageBroker;
    public string conn = "Data Source=api-projetos.db;";
    public RepoProjetos(IMessageBusService messageBroker)
    {
        _messageBroker = messageBroker;
        AocriarProjeto += async (Projeto model) => { await RepoProdutosDisponiveis.AtualizarTabelaProdutosDisponiveis(model); };
        AocriarProjeto += messageBroker.EnviarProjeto;
    }

    public async Task<bool> AtualizarStatus(string model, int? id)
    {
        try
        {
            using var db = new DataContext();
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

    public async Task<Projeto> BuscarPorId(int? id)
    {
        using var connection = new SqliteConnection(conn);
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
            var result = await connection.QueryAsync<Projeto, ProdutosDisponiveis, Projeto>(query,
            map: (p, c) =>
            {
                p.ProdutoUtilizado = c;
                return p;
            },
            param: new { @busca = id },
            splitOn: "ProdutoUtilizadoId");
            return result.FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Response<ProjetoPaginadoDTO>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        var queryPaginado = "SELECT Id, Nome, Status, DataInicio, DataEntrega FROM Projetos LIMIT @resultado OFFSET @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos";

        using var connection = new SqliteConnection(conn);
        var total = Math.Ceiling(await connection.ExecuteScalarAsync<int>(queryTotal) / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<ProjetoPaginadoDTO>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina });
        return new Response<ProjetoPaginadoDTO>(projetosPaginados.ToList(), pagina, (int)total);
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {
        try
        {
            using var db = new DataContext();
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
            using var db = new DataContext();
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
}
