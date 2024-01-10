namespace projeto.domain.Entity.Pessoas;
public class TipoTelefones
{
    public TipoTelefones(
        string descricaoTelefone, 
        Guid pessoaTelefonesId)
    {
        DescricaoTelefone = this.ValidarDescricaco(descricaoTelefone);
        PessoaTelefonesId = pessoaTelefonesId;
    }

    [Key]
    public Guid Id { get; set; }
    
    [DataType("VARCHAR(80)")]
    public string DescricaoTelefone { get; set; }
    public Guid PessoaTelefonesId { get; set; }

    public string ValidarDescricaco(string descricao) {
        var descricaoValidada = ValidadorDeInput.ValidarDescricao(descricao);
        if (!descricaoValidada) return descricao;
        throw new Exception("Não foi possivel estar adicionando a descrição ao endereço");
    }

}
