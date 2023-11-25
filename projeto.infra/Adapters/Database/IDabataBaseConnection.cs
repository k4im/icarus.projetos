namespace projeto.infra.Adapters.Database;

public interface IDataBaseConnection
{
    DataContext ConnectionEntityFrameWorkMysqlProduction();
    MySqlConnection ConnectionForDapperMysqlProduction();

    Task<DataContext> ConnectionEntityFrameWorkSqliteTest();
    SqliteConnection ConnectionDapperSqliteTest();

    
}
