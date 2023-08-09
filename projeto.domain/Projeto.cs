using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.domain;

public class Projeto
{
    protected Projeto()
    { }

    public Projeto(string nome, string status, DateTime dataInicio,
    DateTime dataEntrega,
    int produtoUtilizadoId,
    int quantidadeUtilizado, string descricao, double valor)
    {
        Nome = VerificarNome(nome);
        Status = status;
        DataInicio = dataInicio;
        DataEntrega = dataEntrega;
        ProdutoUtilizadoId = produtoUtilizadoId;
        QuantidadeUtilizado = VerificarQuantidade(quantidadeUtilizado);
        Descricao = VerificarDescricao(descricao);
        Valor = VerificarValor(valor);
    }

    [Key]
    public int Id { get; private set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("NVARCHAR(25)")]
    public string Nome { get; private set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("NVARCHAR(25)")]
    public string Status { get; private set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("DATE")]
    public DateTime DataInicio { get; private set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("DATE")]
    public DateTime DataEntrega { get; private set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("NVARCHAR(150)")]
    public ProdutosDisponiveis ProdutoUtilizado { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType("NVARCHAR(150)")]
    // [ForeignKey("Produtoutilizado")]
    public int ProdutoUtilizadoId { get; private set; }


    [Required(ErrorMessage = "Campo obrigatório")]
    public int QuantidadeUtilizado { get; private set; }

    [DataType("NVARCHAR(150)")]
    public string Descricao { get; private set; }


    [Required(ErrorMessage = "Campo obrigatório")]
    public double Valor { get; private set; }

    [Timestamp]
    public byte[] RowVersion { get; private set; }

    int VerificarQuantidade(int quantidade)
    {
        if (quantidade < 0) throw new Exception("O valor da quantidade não pode ser negativo!");
        return quantidade;
    }

    double VerificarValor(double valor)
    {
        if (valor < 0) throw new Exception("O valor não pode ser negativo!");
        return valor;
    }

    string VerificarDescricao(string desc)
    {
        if (string.IsNullOrEmpty(desc)) throw new Exception("Campo não pode ser nulo");
        if (!Regex.IsMatch(desc, @"^[a-zA-Z ]+$", RegexOptions.Compiled)) throw new Exception("A descrição não pode conter caracteres especiais");
        return desc;
    }
    string VerificarNome(string nome)
    {
        if (string.IsNullOrEmpty(nome)) throw new Exception("Campo não pode ser nulo");
        if (!Regex.IsMatch(nome, @"^[a-zA-Z ]+$", RegexOptions.Compiled)) throw new Exception("O nome não pode conter caracteres especiais");
        return nome;
    }

    public void AtualizarStatus(string status)
        => this.Status = status;
}
