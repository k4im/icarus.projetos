
namespace projeto.infra.Adapters.DatabasePort;
public class DataBaseConnectionMysql : IDataBaseConnection
{
    public DataContext ConnectionEntityFrameWork()
     => new DataContext();
    public DbConnection ConnectionForDapper()
        => new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));

}
