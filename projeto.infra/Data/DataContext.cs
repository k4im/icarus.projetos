namespace projeto.infra.Data;

public class DataContext : DbContext
{
    public DataContext() : base(new DbContextOptionsBuilder().UseSqlite("Data Source=teste.db;").Options)
    {
        Database.EnsureCreated();
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
            .HasMany(produto => produto.Projetos);
    }

    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<ProdutosDisponiveis> ProdutosEmEstoque { get; set; }
}
