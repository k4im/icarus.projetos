namespace projeto.domain;

public class ProjetoDTO
{
    public ProjetoDTO(string nome,
    string status,
    DateTime dataInicio,
    DateTime dataEntrega,
    int produtoUtilizadoId,
    int quantidadeUtilizado,
    string descricao,
    double valor)
    {
        Nome = nome;
        Status = status;
        DataInicio = dataInicio;
        DataEntrega = dataEntrega;
        ProdutoUtilizadoId = produtoUtilizadoId;
        QuantidadeUtilizado = quantidadeUtilizado;
        Descricao = descricao;
        Valor = valor;
    }

    public string Nome { get; }
    public string Status { get; }
    public DateTime DataInicio { get; }
    public DateTime DataEntrega { get; }
    public int ProdutoUtilizadoId { get; }
    public int QuantidadeUtilizado { get; }
    public string Descricao { get; }
    public double Valor { get; }
}
