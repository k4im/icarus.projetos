using System.Xml;
using projeto.domain.Entity.Pessoas;
using projeto.domain.Entity.Usuario;
using projeto.domain.ValueObject.Paises;
using projeto.domain.ValueObject.Pessoas;

namespace projeto.databasePort.Data;
public class DataContext : IdentityDbContext
{
    public DataContext()
    { }

    public DataContext(DbContextOptions options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {   
            var DbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");
            var ServerVersion = new MySqlServerVersion(new Version(8,0,31));
            optionsBuilder.UseMySql(DbConnection, ServerVersion);            
            // optionsBuilder.UseSqlite("Data Source=api-projetos.db");            
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API for specifying concurrency token
        modelBuilder.UseCollation("SQL_LATIN1_GENERAL_CP1_CI_AI");

        modelBuilder.Entity<Projeto>()
            .HasIndex(p => p.Nome);

        modelBuilder.Entity<Projeto>()
            .HasIndex(p => p.Status);

        modelBuilder.Entity<Projeto>()
            .Property(projeto => projeto.RowVersion)
            .IsConcurrencyToken();
        
        modelBuilder.Entity<Pais>()
        .HasMany<Pessoa>(p => p.Pessoas)
        .WithOne(pais => pais.Pais)
        .HasForeignKey(pais => pais.PaisId)
        .HasPrincipalKey(key => key.Id);

        modelBuilder.Entity<Estado>()
        .HasMany<Pessoa>(p => p.Pessoas)
        .WithOne(estado => estado.Estado)
        .HasForeignKey(estado => estado.EstadoId)
        .HasPrincipalKey(key => key.Id);

        modelBuilder.Entity<Municipio>()
        .HasMany<Pessoa>(p => p.Pessoas)
        .WithOne(municipio => municipio.Municipio)
        .HasForeignKey(municipio => municipio.MunicipioId)
        .HasPrincipalKey(key => key.Id);

        modelBuilder.Entity<Profissao>()
        .HasMany<Pessoa>(p => p.Pessoas)
        .WithOne(profissao => profissao.Profissao)
        .HasForeignKey(profissao => profissao.ProfissaoId)
        .HasPrincipalKey(key => key.Id);
        
        modelBuilder.Entity<Telefone>()
        .HasOne<TipoTelefones>(telTipo => telTipo.TipoTelefone)
        .WithMany(tel => tel.PessoaTelefone)
        .HasForeignKey(tel => tel.TipoTelefoneId)
        .HasPrincipalKey(key => key.Id);

        modelBuilder.Entity<Endereco>()
        .HasOne<TipoDeEndereco>(endTipo => endTipo.TipoEndereco)
        .WithMany(endTipo => endTipo.Endereco)
        .HasForeignKey(end => end.TipoEnderecoId)
        .HasPrincipalKey(key => key.Id);

    }
    // Tabelas relacionada a pessoas
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<TipoTelefones> TipoTelefones { get; set; }
    public DbSet<TipoDeEndereco> TipoEnderecos { get; set; }
    public DbSet<Sexo> Sexo { get; set; }
    public DbSet<EstadoCivil> EstadoCivis { get; set; }
    public DbSet<Profissao> Profissoes { get; set; }
    public DbSet<Conselho> Conselhos { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    
    // Tabela de projetos/serviços 
    public DbSet<Projeto> Projetos { get; set; }
    
}