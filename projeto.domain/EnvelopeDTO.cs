namespace projeto.domain;

public class EnvelopeDTO
{
    public EnvelopeDTO(int produtoUtilizado, int quantidadeUtilizado, string correlationID)
    {
        ProdutoUtilizado = produtoUtilizado;
        QuantidadeUtilizado = quantidadeUtilizado;
        CorrelationID = correlationID;
    }

    public int ProdutoUtilizado { get; set; }
    public int QuantidadeUtilizado { get; set; }
    public string CorrelationID { get; set; }
}

