using projeto.domain.Entity.Pessoas;

namespace projeto.domain.ValueObject.Paises;
public class Pais
{   [Key]
    public Guid Id { get; set; }

    [DataType("VARCHAR(50)")]
    public string Nome { get; set; }
    [DataType("VARCHAR(20)")]
    public string Gentilico { get; set; }

    [DataType("VARCHAR(3)")]
    public string Sigla { get; set; }

    // Id De referencia
    public ICollection<Pessoa> Pessoas { get; set; }
}