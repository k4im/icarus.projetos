namespace projeto.domain.Dtos;
public record EnvelopeDTO{
    public EnvelopeDTO(int produtoUtilizado, int quantidadeUtilizado, string correlationID)
    {
        ProdutoUtilizado = produtoUtilizado;
        QuantidadeUtilizado = quantidadeUtilizado;
        CorrelationID = correlationID;
    }

    public int ProdutoUtilizado {get; } 
    public int QuantidadeUtilizado {get; } 
    public string CorrelationID {get; }
};
