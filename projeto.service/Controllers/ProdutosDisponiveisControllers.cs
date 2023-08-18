namespace projeto.service.Controllers;

[ApiController]
[Route("api/")]
public class ProdutosDisponiveisControllers : ControllerBase
{
    readonly IRepoProdutosDisponiveis _repo;

    public ProdutosDisponiveisControllers(IRepoProdutosDisponiveis repo)
    {
        _repo = repo;
    }

    [HttpGet("produtosEmEstoque")]
    public async Task<ActionResult<List<ProdutosDisponiveis>>> mostrarProdutos()
    {
        var produtos = await _repo.BuscarTodosProdutos();
        return Ok(produtos);
    }
}
