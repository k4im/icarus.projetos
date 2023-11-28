namespace projeto.infra.Adapters.DatabasePort;
public class DataBaseConnectionSqlite : IDataBaseConnection
{
    IConfiguration _config;
    public DataBaseConnectionSqlite(IConfiguration config)
        => _config = config;
    public DataContext ConnectionEntityFrameWork()
        => new DataContext(
            new DbContextOptionsBuilder()
                .UseSqlite(_config.GetConnectionString("SqliteDatabase"))
                .Options);
    public DbConnection ConnectionForDapper()
        => new SqliteConnection(_config.GetConnectionString("SqliteDatabase"));
}