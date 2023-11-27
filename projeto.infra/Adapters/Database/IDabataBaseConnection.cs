namespace projeto.infra.Adapters.Database;

public interface IDataBaseConnection
{
    DataContext ConnectionEntityFrameWork();
    MySqlConnection ConnectionForDapper();
    
}
