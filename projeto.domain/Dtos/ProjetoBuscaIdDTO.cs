namespace projeto.domain.Dtos;
public record ProjetoBuscaIdDTO(
    string Nome,
    string Status,
    DateTime DataInicio,
    DateTime DataEntrega,
    ProdutoEmEstoqueDTO ProdutoUtilizado,
    int QuantidadeUtilizado,
    string Descricao,
    double Valor
);
