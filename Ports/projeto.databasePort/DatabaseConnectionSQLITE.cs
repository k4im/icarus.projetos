using Microsoft.Data.Sqlite;

namespace projeto.databasePort;
public class DatabaseConnectionSQLITE : IDataBaseConnection
{
    public DataContext ConnectionEntityFrameWork()
        => new DataContext(new DbContextOptionsBuilder()
            .UseSqlite("Data Source=api-projetos.db;")
            .Options);
    public DbConnection ConnectionForDapper()
        => new SqliteConnection("Data Source=api-projetos.db;");
}
