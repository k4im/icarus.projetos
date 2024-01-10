namespace projeto.domain.ValueObject.Pessoas;
public class Sexo
{
    public Sexo(
        string nome, 
        string sigla)
    {
        Nome = nome;
        Sigla = sigla;
    }

    [Key]
    public Guid Id { get; set; }
    
    [DataType("VARCHAR(20)")]
    public string Nome { get; set; }
    [DataType("VARCHAR(5)")]
    public string Sigla { get; set; }
}