namespace projeto.infra.Data;
public class DataContext : DbContext
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
            Database.EnsureCreated();
            
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

        modelBuilder.Entity<Projeto>()
            .HasOne(projeto => projeto.ProdutoUtilizado)
            .WithMany(projeto => projeto.Projetos)
            .HasForeignKey(projeto => projeto.ProdutoUtilizadoId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(true);

        modelBuilder.Entity<ProdutosDisponiveis>()
            .HasMany(produto => produto.Projetos)
            .WithOne(projeto => projeto.ProdutoUtilizado)
            .HasForeignKey(f => f.ProdutoUtilizadoId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(true);
    }

    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<ProdutosDisponiveis> ProdutosEmEstoque { get; set; }
}
