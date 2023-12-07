namespace projeto.domain.Dtos;
public record ProdutoEmEstoqueDTO {
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
};