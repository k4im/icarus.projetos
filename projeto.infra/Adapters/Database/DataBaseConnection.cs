
namespace projeto.infra.Adapters.Database;
public class DataBaseConnection : IDataBaseConnection
{
    IConfiguration _config;

    public DataBaseConnection(IConfiguration config)
        => _config = config;

    public DataContext ConnectionEntityFrameWork()
     => new DataContext();
    public MySqlConnection ConnectionForDapper()
        => new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));

}
