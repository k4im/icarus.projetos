namespace projeto.domain.ValueObject.Paises;
public class Estado
{   
    [Key]
    public Guid Id { get; set; }
    [DataType("VARCHAR(50)")]
    public string Nome { get; set; }
    [DataType("VARCHAR(20)")]
    public string Gentilico { get; set; }

    [DataType("VARCHAR(3)")]
    public string Sigla { get; set; }
    
    //Chave estrangeira
    public Guid PaisId { get; set; }

}