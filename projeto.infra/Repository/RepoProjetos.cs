
namespace projeto.infra.Repository;
public class RepoProjetos : IRepoProjetos
{
    //Delegates e Eventos a serem disparados
    public delegate void aoCriarProjetoEventHandler(Projeto model);
    public event aoCriarProjetoEventHandler aocriarProjeto;
    IMessageBusService _messageBroker;

    public RepoProjetos(IMessageBusService messageBroker)
    {
        _messageBroker = messageBroker;
        aocriarProjeto += async (Projeto model) => { await RepoProdutosDisponiveis.atualizarTabelaProdutosDisponiveis(model); };
        aocriarProjeto += messageBroker.enviarProjeto;
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
        using var connection = new SqliteConnection("Data Source=teste.db;");
        try
        {
            var query = "SELECT * FROM Projetos WHERE Id LIKE @busca";
            var result = await connection.QueryFirstAsync<Projeto>(query, new { busca = id });
            return result;
        }
        catch(Exception)
        {
            return null; 
        }
    }

    public async Task<Response<Projeto>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        var queryPaginado = "SELECT * FROM Projetos LIMIT @resultado OFFSET @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos";

        using var connection = new SqliteConnection("Data Source=teste.db;");
        var total = Math.Ceiling(await connection.ExecuteScalarAsync<int>(queryTotal) / resultadoPorPagina);
        var projetosPaginados = await connection
            .QueryAsync<Projeto>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina });
        return new Response<Projeto>(projetosPaginados.ToList(), pagina, (int)total);
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {
        try
        {
            using var db = new DataContext();
            if (await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado) == null)
            {
                throw new Exception($"Enterrompido criação do projeto pois produto com [id] - [{model.ProdutoUtilizado}] não existe!");
            }
            db.Projetos.Add(model);
            await db.SaveChangesAsync();
            aocriarProjeto(model);
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
