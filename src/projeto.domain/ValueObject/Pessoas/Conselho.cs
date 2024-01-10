namespace projeto.domain.ValueObject.Pessoas;
public class Conselho
{
    [Key]
    public Guid Id { get; set; }
    [DataType("VARCHAR(80)")]
    public string Nome { get; set; }
    [DataType("VARCHAR(20)")]
    public string Sigla { get; set; }
}
