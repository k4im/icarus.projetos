namespace projeto.benchmarks.Helpers;

public class FakeProjeto
{
    public static Projeto factoryProjeto()
    {
        var status = factoryProjetoStatus();
        return new Projeto("Projeto FAKE", status, DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "asd", 155.5);
    }

    static StatusProjeto factoryProjetoStatus() => new StatusProjeto("Rodando");
    public static List<Projeto> factoryListaProjetos()
    {
        return new List<Projeto>{
                factoryProjeto(),
                factoryProjeto(),
                factoryProjeto(),
                factoryProjeto(),
                factoryProjeto()
            };
    }
}
