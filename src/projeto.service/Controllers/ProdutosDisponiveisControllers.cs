using Microsoft.AspNetCore.Authorization;

namespace projeto.service.Controllers;

[ApiController]
[Route("api/")]
public class ProdutosDisponiveisControllers : ControllerBase
{
    // readonly IRepoProdutosDisponiveis _repo;

    // public ProdutosDisponiveisControllers(IRepoProdutosDisponiveis repo)
    // {
    //     _repo = repo;
    // }

    // [HttpGet("produtosEmEstoque")]
    // [Authorize(Roles = "ADMIN")]
    // public async Task<ActionResult<List<ProdutosDisponiveis>>> MostrarProdutos()
    // {
    //     // var produtos = await _repo.BuscarTodosProdutos();
    //     return Ok(produtos);
    // }
}
