namespace projeto.domain.Dtos;
public record ProjetoDTO(
    string Nome,
    string Status,
    DateTime DataInicio,
    DateTime DataEntrega,
    int ProdutoUtilizadoId,
    int QuantidadeUtilizado,
    string Descricao,
    double Valor
);
