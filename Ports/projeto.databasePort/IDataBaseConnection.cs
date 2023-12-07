namespace projeto.databasePort;
public interface IDataBaseConnection
{
    DataContext ConnectionEntityFrameWork();
    DbConnection ConnectionForDapper();
}
