using System.Data.Common;
using Microsoft.Data.Sqlite;
using projeto.databasePort.Data;

namespace projeto.databaseAdapters;
public class DatabaseConnectionSQLITE : IDataBaseConnection
{
    public DataContext ConnectionEntityFrameWork()
        => new DataContext(new DbContextOptionsBuilder()
            .UseSqlite("Data Source=api-projetos.db;")
            .Options);
    public DbConnection ConnectionForDapper()
        => new SqliteConnection("Data Source=api-projetos.db;");
}
