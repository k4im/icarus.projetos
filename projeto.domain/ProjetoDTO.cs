namespace projeto.domain;

public class ProjetoDTO
{
    public string Nome { get; set; }
    public string Status { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataEntrega { get; set; }
    public int ProdutoUtilizadoId { get; set; }
    public int QuantidadeUtilizado { get; private set; }
    public string Descricao { get; private set; }
    public double Valor { get; private set; }
}
