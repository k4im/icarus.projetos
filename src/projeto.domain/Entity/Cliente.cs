using projeto.domain.ValueObject;

namespace projeto.domain.Entity;
public class Cliente
{
    public Cliente(
        string nome, 
        string sobrenome, 
        Telefone contato, 
        Endereco endereco)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Contato = contato;
        Endereco = endereco;
    }

    public int Id { get; }
    public string Nome { get; }
    public string? Sobrenome { get; }
    public Telefone Contato { get; }
    public Endereco Endereco{ get; }
}
