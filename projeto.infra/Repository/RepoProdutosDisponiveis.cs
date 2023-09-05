using MySqlConnector;

namespace projeto.infra.Repository;

public class RepoProdutosDisponiveis : IRepoProdutosDisponiveis
{
    public string conn = Environment.GetEnvironmentVariable("DB_CONNECTION");
    public static string conn2 = Environment.GetEnvironmentVariable("DB_CONNECTION");

    public async Task<bool> AdicionarProdutos(ProdutosDisponiveis model)
    {
        try
        {
            using var db = new DataContext();
            db.ProdutosEmEstoque.AddRange(model);
            await db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel realizar a operação, a mesma já foi realizado por um outro usuario!");
            return false;
        }
        catch (ArgumentException)
        {
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação: {e.Message}");
            return false;
        }
    }

    public async Task<bool> AtualizarProdutos(int id, ProdutosDisponiveis model)
    {
        try
        {
            using var db = new DataContext();
            await db.ProdutosEmEstoque.Where(x => x.Id == id)
            .ExecuteUpdateAsync(setter =>
                setter.SetProperty(p => p.Nome, model.Nome)
                .SetProperty(p => p.Quantidade, model.Quantidade));
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

    public async Task<List<ProdutoEmEstoqueDTO>> BuscarTodosProdutos()
    {
        using var connection = new MySqlConnection(conn);
        var query = "SELECT * FROM ProdutosEmEstoque";
        try
        {
            var produtos = await connection.QueryAsync<ProdutoEmEstoqueDTO>(query);
            return produtos.ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> RemoverProdutos(int id)
    {
        try
        {
            using var db = new DataContext();
            await db.ProdutosEmEstoque.Where(x => x.Id == id)
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

    public static async Task AtualizarTabelaProdutosDisponiveis(Projeto model)
    {
        using var db = new DataContext();
        var produto = await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizadoId);
        produto.Quantidade -= model.QuantidadeUtilizado;
        if(produto.Quantidade <= 0) {
            db.Remove(produto);
            
        }
        await db.SaveChangesAsync();
    }

    public static ProdutosDisponiveis BuscarPorId(int id)
    {
        var query = "SELECT * FROM Produtos WHERE Id LIKE @busca";
        try
        {
            using var connc = new MySqlConnection(conn2);
            var resultado = connc.QueryFirstOrDefault<ProdutosDisponiveis>(query,
                new { busca = id });
            return resultado;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
