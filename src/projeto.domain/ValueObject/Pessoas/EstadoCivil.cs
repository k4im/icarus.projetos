namespace projeto.domain.ValueObject.Pessoas;
public class EstadoCivil
{
    [Key]
    public Guid Id { get; set; }
    [DataType("VARCHAR(80)")]
    public string Nome { get; set; }
}
