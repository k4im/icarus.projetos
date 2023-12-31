namespace projeto.domain.Dtos;
public record ProjetoPaginadoDTO{
    public int Id {get; set; }
    public string Nome {get; set; } 
    public string Status {get; set; } 
    public DateTime DataInicio {get; set; }
    public DateTime DataEntrega {get; set; }
    public double Valor {get; set; }
};