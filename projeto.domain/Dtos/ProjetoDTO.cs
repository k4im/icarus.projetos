namespace projeto.domain.Dtos;
public record ProjetoDTO {       
    public string Nome {get; set;}
    public string Status {get; set;}
    public DateTime DataInicio {get; set;}
    public DateTime DataEntrega {get; set;}
    public int ProdutoUtilizadoId {get; set;}
    public int QuantidadeUtilizado {get; set;}
    public string Descricao {get; set;}
    public double Valor {get; set; }
}