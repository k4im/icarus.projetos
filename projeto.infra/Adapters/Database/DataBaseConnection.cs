
namespace projeto.infra.Adapters.Database;
public class DataBaseConnection : IDataBaseConnection
{
    IConfiguration _config;

    public DataBaseConnection(IConfiguration config)
        => _config = config;

    public DataContext ConnectionEntityFrameWorkMysqlProduction()
     => new DataContext();
    public MySqlConnection ConnectionForDapperMysqlProduction()
        => new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));


    public async Task<DataContext> ConnectionEntityFrameWorkSqliteTest()
    {
        var Connection = new DataContext(
            new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(_config
            .GetConnectionString("TesteDataBase"))
            .Options
        );
        await Connection.Database.EnsureCreatedAsync();
        return Connection;
    }
    public SqliteConnection ConnectionDapperSqliteTest()
        => new SqliteConnection(_config
            .GetConnectionString("TesteDataBase"));

}
