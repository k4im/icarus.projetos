namespace projeto.domain.Entity.Usuario;
public class Usuario : IdentityUser
{
    public bool Inativo { get; set; } = false;
    public Guid PessoaId { get; set; }

    void InativarUsuario() 
        => this.Inativo = true;
}
