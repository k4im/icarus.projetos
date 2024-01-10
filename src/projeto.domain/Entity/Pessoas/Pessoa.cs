namespace projeto.domain.Entity.Pessoas;
public class Pessoa
{
    [Key]
    public Guid Id { get; set; }

    [DataType("VARCHAR(120)")]
    public string Nome { get; set; }

    [DataType("VARCHAR(50)")]
    public string Apelido { get; set; }

    [DataType("VARCHAR(15)")]
    public string Matricula { get; set; }

    public int Codigo { get; set; }

    public DateTime DataInclusao { get; set; }

    public bool Inativo { get; set; }

    public bool Estrangeiro { get; set; }

    [DataType("VARCHAR(250)")]
    public string Email { get; set; }

    public bool Cliente { get; set; }

    public bool Colaborador { get; set; }

    public bool Fornecedor { get; set; }

    public DateTime DataNascimento { get; set; }

      [DataType("VARCHAR(120)")]
    public string Pai { get; set; }

    [DataType("VARCHAR(120)")]
    public string Mae { get; set; }

    public Guid SexoId { get; set; }

    [DataType("VARCHAR(120)")]
    public string Rg { get; set; }

    [DataType("VARCHAR(120)")]
    public string Emissor { get; set; }

    [DataType("VARCHAR(120)")]
    public string UFEmessior { get; set; }

    [DataType("VARCHAR(120)")]
    public string DataRg { get; set; }

    [DataType("VARCHAR(120)")]
    public string Cpf { get; set; }

    [DataType("VARCHAR(120)")]
    public string Ctps { get; set; }

    [DataType("VARCHAR(120)")]
    public string DataCtps { get; set; }

    [DataType("VARCHAR(120)")]
    public string Nrpis { get; set; }

    [DataType("VARCHAR(120)")]
    public string DataPis { get; set; }

    [DataType("VARCHAR(120)")]
    public string RegProfNumero { get; set; }

    [DataType("VARCHAR(120)")]
    public string Conselho { get; set; }

    [DataType("VARCHAR(120)")]
    public string UFConselho { get; set; }

    [DataType("VARCHAR(120)")]
    public string RegProfSerie { get; set; }

    [DataType("VARCHAR(120)")]
    public string Profissao { get; set; }

    [DataType("VARCHAR(120)")]
    public string Dependentes  { get; set; }

    [DataType("VARCHAR(120)")]
    public string RazaoSocial { get; set; }

    [DataType("VARCHAR(120)")]
    public string Cnpj { get; set; }

    [DataType("VARCHAR(120)")]
    public string InscricaoEstadual { get; set; }

    [DataType("VARCHAR(120)")]
    public string InscricaoMunicipal { get; set; }

    [DataType("VARCHAR(120)")]
    public string ObjetoSocial { get; set; }

    [DataType("VARCHAR(120)")]
    public string Observacoes { get; set; }


    //Chaves estrangeiras
    public Guid PaisId { get; set; }

    public Guid EstadoId { get; set; }

    public Guid MunicipioId { get; set; }

    public Guid EstadoCivilId { get; set; }
}
