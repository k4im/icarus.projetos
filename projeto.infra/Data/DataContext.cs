namespace projeto.infra.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=api-projetos.db;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API for specifying concurrency token
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
