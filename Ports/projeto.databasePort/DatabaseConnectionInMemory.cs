
using Microsoft.Data.Sqlite;
namespace projeto.databasePort;
public class DatabaseConnectionInMemory : IDataBaseConnection
{
    public DataContext ConnectionEntityFrameWork()
        => new DataContext(new DbContextOptionsBuilder()
        .UseInMemoryDatabase("Database")
        .Options);
    public DbConnection ConnectionForDapper()
        => new SqliteConnection(
            "Data Source=:memory:"
        );
}