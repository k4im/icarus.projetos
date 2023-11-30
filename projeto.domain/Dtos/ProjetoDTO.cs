namespace projeto.domain.Dtos;
public record ProjetoDTO {       
    public string Nome {get;}
    public string Status {get;}
    public DateTime DataInicio {get;}
    public DateTime DataEntrega {get;}
    public int ProdutoUtilizadoId {get;}
    public int QuantidadeUtilizado {get;}
    public string Descricao {get;}
    public double Valor {get; }
}