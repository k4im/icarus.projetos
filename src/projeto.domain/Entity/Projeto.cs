using System.Runtime.CompilerServices;
using projeto.domain.Validators;

namespace projeto.domain.Entity;

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
        Descricao = descricao;
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
        if (ValidadorDeInput.ValidarMenorDoQueZero(quantidade)) throw new Exception("O valor da quantidade não pode ser negativo!");
        return quantidade;
    }

    double VerificarValor(double valor)
    {
        if (ValidadorDeInput.ValidarMenorDoQueZero(valor)) throw new Exception("O valor não pode ser negativo!");
        return valor;
    }
    string VerificarNome(string nome)
    {
        if (ValidadorDeInput.ValidarInputNulo(nome)) throw new Exception("Campo não pode ser nulo");
        if (ValidadorDeInput.ValidarInputRegex(nome)) throw new Exception("O nome não pode conter caracteres especiais");
        return nome;
    }

    public void AtualizarObjeto(Projeto model) {
        if(this.Nome != model.Nome) this.Nome = model.Nome;
        if(this.Status != model.Status) this.Status = model.Status;
        if(this.DataInicio != model.DataInicio) this.DataInicio = model.DataInicio;
        if(this.DataEntrega != model.DataEntrega) this.DataEntrega = model.DataEntrega;
        if(this.ProdutoUtilizadoId != model.ProdutoUtilizadoId) this.ProdutoUtilizadoId = model.ProdutoUtilizadoId;
        if(this.QuantidadeUtilizado != model.QuantidadeUtilizado) this.QuantidadeUtilizado = model.QuantidadeUtilizado;
        if(this.Descricao != model.Descricao) this.Descricao = model.Descricao;
        if(this.Valor != model.Valor) this.Valor = model.Valor;

    }
}
