
namespace projeto.infra.Adapters.Database;
public class DataBaseConnectionMysql : IDataBaseConnection<MySqlConnection>
{
    IConfiguration _config;

    public DataBaseConnectionMysql(IConfiguration config)
        => _config = config;

    public DataContext ConnectionEntityFrameWork()
     => new DataContext();
    public MySqlConnection ConnectionForDapper()
        => new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));

}
