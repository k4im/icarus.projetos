namespace projeto.infra.Repository;

public class RepoProdutosDisponiveis : IRepoProdutosDisponiveis
{
    public string conn = "Data Source=teste.db;";

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

    public async Task<Response<ProdutosDisponiveis>> BuscarTodosProdutos()
    {
        using var connection = new SqliteConnection(conn);
        var query = "SELECT * FROM ProdutosDispoinveis";
        try
        {
            var produtos = await connection.QueryAsync<ProdutosDisponiveis>(query);
            return new Response<ProdutosDisponiveis>(produtos.ToList(), 1, 2);
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
        var produto = await db.ProdutosEmEstoque.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizadoId);
        produto.Quantidade -= model.QuantidadeUtilizado;
        await db.SaveChangesAsync();
    }

    public async Task<ProdutosDisponiveis> BuscarPorId(int id)
    {
        try
        {
            using var connc = new SqliteConnection(conn);
            var resultado = await connc.QueryFirstOrDefaultAsync<ProdutosDisponiveis>("SELECT * FROM Produtos WHERE Id LIKE @busca",
                new { busca = id });
            return resultado;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
