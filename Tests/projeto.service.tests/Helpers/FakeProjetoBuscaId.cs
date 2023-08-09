namespace projeto.service.tests.Helpers;

public class FakeProjetoBuscaId
{
    public static ProjetoBuscaIdDTO FactoryProjeto()
           => new ProjetoBuscaIdDTO
           {
               Nome = "fake",
               Status = "Teste",
               DataInicio = DateTime.Now,
               DataEntrega = DateTime.Now.AddHours(1),
               Descricao = "Teste",
               ProdutoUtilizado = new ProdutoEmEstoqueDTO { Nome = "Fake", Quantidade = 5 },
               QuantidadeUtilizado = 5,
               Valor = 150
           };
}