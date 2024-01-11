namespace projeto.domain.ValueObject.Paises;
public class Municipio
{
    [Key]
    public Guid Id { get; set; }
    
    [DataType("VARCHAR(30)")]
    public string Nome { get; set; }
    [DataType("VARCHAR(15)")]
    public string Cep { get; set; }
    [DataType("VARCHAR(7)")]
    public string CodigoIbge { get; set; }

    //Chaves estrangeiras
    public Guid PaisId { get; set; }
    public Guid EstadoId { get; set; }
    public ICollection<Pessoa> Pessoas { get; set; }

}