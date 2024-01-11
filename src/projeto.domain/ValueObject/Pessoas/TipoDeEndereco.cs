namespace projeto.domain.ValueObject.Pessoas;
public class TipoDeEndereco
{
    public TipoDeEndereco(
        string descricaoEndereco, 
        Guid enderecoId)
    {
        DescricaoEndereco = this.ValidarDescricaco(descricaoEndereco);
        EnderecoId = enderecoId;
    }

    [Key]
    public Guid Id { get; set; }
    
    [DataType("VARCHAR(80)")]
    public string DescricaoEndereco { get; set; }
    
    public ICollection<Endereco> Endereco { get; set; }
    public Guid EnderecoId { get; set; }

    public string ValidarDescricaco(string descricao) {
        var descricaoValidada = ValidadorDeInput.ValidarDescricao(descricao);
        if (!descricaoValidada) return descricao;
        throw new Exception("Não foi possivel estar adicionando a descrição ao endereço");
    }

}
