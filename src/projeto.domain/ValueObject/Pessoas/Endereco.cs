namespace projeto.domain.ValueObject.Pessoas;
public class Endereco
{
    public Endereco(
        string logadouro, 
        string complemento, 
        string bairro, 
        string caixaPostal, 
        string cep, 
        bool enderecoPrincipal, 
        string observacoes,
        Guid pais, 
        Guid estado, 
        Guid municipio, 
        Guid pessoaId)
    {
        PessoaId = pessoaId;
        Logadouro = logadouro;
        Complemento = complemento;
        Bairro = bairro;
        CaixaPostal = caixaPostal;
        Pais = pais;
        Estado = estado;
        Municipio = municipio;
        Cep = ValidarCep(cep);
        EnderecoPrincipal = enderecoPrincipal;
        Observacoes = observacoes;
    }

    [Key]
    public Guid Id { get; set; }
    [DataType("VARCHAR(100)")]
    public string Logadouro { get; set; }
    [DataType("VARCHAR(100)")]
    public string Complemento { get; set; }
    [DataType("VARCHAR(50)")]
    public string Bairro { get; set; }
    [DataType("VARCHAR(10)")]
    public string CaixaPostal { get; set; }
    [DataType("VARCHAR(15)")]
    public string Cep { get; set; }
    public bool EnderecoPrincipal { get; set; }
    public string Observacoes { get; set; }
    
    // Chaves estrangeiras
    public Guid TipoEnderecoId { get; set; }
    public Guid Pais { get; set; }
    public Guid Estado { get; set; }
    public Guid Municipio { get; set; }
    public Guid PessoaId { get; set; }
    
    string ValidarCep(string cep) {
        var cepLimpo = cep.Trim()
        .Replace(".", "")
        .Replace("-", "");
        if (!Regex.IsMatch(cep, @"^[0-9 ]+$", RegexOptions.Compiled))
            throw new Exception("O cep precisa conter apenas numeros");
        return cepLimpo;
    }

}
