using Dapper;
using Microsoft.Data.Sqlite;
using projeto.infra.Helpers;

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
        var query = "SELECT * FROM Projetos WHERE Id LIKE @busca";
        return await connection.QueryFirstAsync<Projeto>(query, new { busca = id });
    }

    public async Task<Response<Projeto>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        var queryPaginado = $"SELECT * FROM Projetos LIMIT @resultado OFFSET @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos";

        using var connection = new SqliteConnection("Data Source=teste.db;");
        var total = await connection.ExecuteScalarAsync<int>(queryTotal);
        var projetosPaginados = await connection
            .QueryAsync<Projeto>(queryPaginado, new { resultado = resultadoPorPagina, pagina = (pagina - 1) * resultadoPorPagina });
        return new Response<Projeto>(projetosPaginados.ToList(), pagina, total);
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {
        try
        {
            using var db = new DataContext();
            // if (await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado) == null)
            // {
            //     throw new Exception($"Enterrompido criação do projeto pois produto com [id] - [{model.ProdutoUtilizado}] não existe!");
            // }
            for (int i = 0; i < 1000; i++)
                db.Projetos.AddRange(FakeProjeto.factoryListaProjetos());
            await db.SaveChangesAsync();
            aocriarProjeto(model);
            return true;

        }
        catch (ArgumentException)
        {
            //Retorna true devido ao problema de adição em massa de dados
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
            var item = await BuscarPorId(id);
            db.Projetos.Remove(item);
            await db.SaveChangesAsync();
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
