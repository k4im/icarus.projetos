using Microsoft.Data.Sqlite;
using projeto.infra.Helpers;

namespace projeto.infra.Repository;


public class RepoProjetos : IRepoProjetos
{
    //Delegates e Eventos a serem disparados
    public delegate void aoCriarProjetoEventHandler(Projeto model);
    public event aoCriarProjetoEventHandler aocriarProjeto;
    // IMessageBusService _messageBroker;

    public RepoProjetos(IMessageBusService messageBroker)
    {
        // _messageBroker = messageBroker;
        // aocriarProjeto += async (Projeto model) => { await RepoProdutosDisponiveis.atualizarTabelaProdutosDisponiveis(model); };
        // aocriarProjeto += messageBroker.enviarProjeto;
    }

    public async Task<bool> AtualizarStatus(StatusProjeto model, int? id)
    {
        try
        {
            using (var db = new DataContext())
            {
                var projeto = await db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                projeto.AtualizarStatus(model);
                await db.SaveChangesAsync();
                return true;
            }

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
        using (var db = new DataContext())
        {
            var item = await db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

    }

    public async Task<Response<Projeto>> BuscarProdutos(int pagina, float resultadoPorPagina)
    {
        using (var db = new DataContext())
        {
            var total = await db.Projetos.FromSql($"SELECT * FROM Projetos").CountAsync();
            var projetosPaginados = await db.Projetos.FromSql($"SELECT * FROM Projetos LIMIT {resultadoPorPagina} OFFSET {(pagina -1) * resultadoPorPagina}").ToListAsync();
            return new Response<Projeto>(projetosPaginados, 1, total);
        }
    }

    public async Task<bool> CriarProjeto(Projeto model)
    {        try
        {
            using (var db = new DataContext())
            {
                // if (await db.ProdutosEmEstoque.FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado) == null)
                // {
                //     throw new Exception($"Enterrompido criação do projeto pois produto com [id] - [{model.ProdutoUtilizado}] não existe!");
                // }
                for (int i = 0; i < 1000; i++)
                    db.Projetos.AddRange(FakeProjeto.factoryListaProjetos());
                await db.SaveChangesAsync();
                // aocriarProjeto(model);
                return true;
            }

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
            using (var db = new DataContext())
            {
                var item = await BuscarPorId(id);
                db.Projetos.Remove(item);
                await db.SaveChangesAsync();
                return true;
            }

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
