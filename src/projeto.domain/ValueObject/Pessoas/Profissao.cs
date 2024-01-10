namespace projeto.domain.ValueObject.Pessoas;
public class Profissao
{
    [Key]
    public Guid Id { get; set; }
    
    [DataType("VARCHAR(80)")]
    public string Nome { get; set; }
    public Guid ConselhoId { get; set; }
}
