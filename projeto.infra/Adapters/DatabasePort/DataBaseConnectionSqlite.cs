namespace projeto.infra.Adapters.DatabasePort;
public class DataBaseConnectionSqlite : IDataBaseConnection
{
    public DataContext ConnectionEntityFrameWork()
        => new DataContext(
            new DbContextOptionsBuilder()
                .UseSqlite("Data Source=api-projetos.db")
                .Options);
    public DbConnection ConnectionForDapper()
        => new SqliteConnection("Data Source=api-projetos.db");
}