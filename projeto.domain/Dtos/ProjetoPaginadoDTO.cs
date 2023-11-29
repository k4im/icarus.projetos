namespace projeto.domain.Dtos;
public record ProjetoPaginadoDTO(
    int Id,
    string Nome, 
    string Status, 
    DateTime DataInicio,
    DateTime DataEntrega,
    double Valor
);