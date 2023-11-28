namespace projeto.infra.Adapters.DatabasePort;

public interface IDataBaseConnection
{
    DataContext ConnectionEntityFrameWork();
    DbConnection ConnectionForDapper();
}
