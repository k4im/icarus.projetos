namespace projeto.service.tests.Helpers;
public class FakeProjetoPaginado
{

    public static ProjetoPaginadoDTO FactoryProjeto()
           => new ProjetoPaginadoDTO
           {
               Id = 1,
               Nome = "fake",
               Status = "Teste",
               DataInicio = DateTime.Now,
               DataEntrega = DateTime.Now.AddHours(1),
               Valor = 150
           };

    public static List<ProjetoPaginadoDTO> FactoryListaProjetos()
    {
        return new List<ProjetoPaginadoDTO>{
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto()
            };
    }
}