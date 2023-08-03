namespace projeto.infra.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base(new DbContextOptionsBuilder().UseSqlite("Data Source=teste.db;").Options)
        { 
            Database.EnsureCreated();
        }

        // public DataContext(DbContextOptions options) : base(options)
        // { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API for specifying concurrency token
            modelBuilder.Entity<Projeto>()
                .Property(projeto => projeto.RowVersion)
                .IsConcurrencyToken();


            modelBuilder.Entity<Projeto>(builder =>
            {
                builder.OwnsOne<StatusProjeto>(projeto => projeto.Status)
                .Property(status => status.Status)
                .HasColumnName("Status");
            });
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<ProdutosDisponiveis> ProdutosEmEstoque { get; set; }
    }
}