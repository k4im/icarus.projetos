namespace projeto.domain.Dtos;
public record EnvelopeDTO(
    int ProdutoUtilizado, 
    int QuantidadeUtilizado, 
    string CorrelationID
);
