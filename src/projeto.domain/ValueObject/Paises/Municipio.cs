using projeto.domain.ValueObject.Pessoas;

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
    public Pais Pais { get; set; }
    public Guid PaisId { get; set; }
    
    public Estado Estado { get; set; }
    public Guid EstadoId { get; set; }
    
    public Municipio MunicipioObj { get; set; }
    public Guid MunicipioId { get; set; }
    
    public ICollection<Pessoa> Pessoas { get; set; }
    public ICollection<Endereco> Enderecos { get; set; }

}