namespace projeto.infra.Adapters.Database;

public interface IDataBaseConnection<T>
{
    DataContext ConnectionEntityFrameWork();
    T ConnectionForDapper();
    
}
