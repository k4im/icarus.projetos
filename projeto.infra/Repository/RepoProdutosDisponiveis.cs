namespace projeto.infra.Repository;

public class RepoProdutosDisponiveis : IRepoProdutosDisponiveis
{
    public string conn = "Data Source=teste.db;";

    public async Task<bool> adicionarProdutos(ProdutosDisponiveis model)
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

    public async Task<bool> atualizarProdutos(int id, ProdutosDisponiveis model)
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

    public async Task<List<ProdutosDisponiveis>> buscarTodosProdutos()
    {
        using var connection = new SqliteConnection(conn);
        var query = "SELECT * FROM ProdutosDispoinveis";
        try
        {
            var produtos = await connection.QueryAsync<ProdutosDisponiveis>(query);
            return produtos.ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> removerProdutos(int id)
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
        var produto = await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado);
        produto.Quantidade -= model.QuantidadeUtilizado;
        await db.SaveChangesAsync();
    }
}
