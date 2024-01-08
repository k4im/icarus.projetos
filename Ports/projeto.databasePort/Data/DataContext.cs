
namespace projeto.databasePort.Data;
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
    }

    public DbSet<Projeto> Projetos { get; set; }
}