namespace projeto.domain;

public class ProdutosDisponiveis
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();
}
