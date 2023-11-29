using projeto.infra.Adapters.DatabaseAdapter;
using projeto.infra.Adapters.DatabasePort;

namespace projeto.service.tests;
public class DataBaseAdapterTest
{
    [Fact]
    public async void Deve_realizar_operacao_select_no_banco()
    {
        //Arrange
        var Adapter = new DatabaseAdapterProjeto(new DataBaseConnectionSqlite());
        var queryPaginado = @"
        SELECT 
            Id, 
            Nome, 
            Status, 
            DataInicio, 
            DataEntrega, 
            Valor 
        FROM 
            Projetos 
        LIMIT 
            5 
        OFFSET 
            1";
        var queryTotal = "SELECT count(*) FROM Projetos";

        //Act 
        var result = await Adapter.PaginarProjetosDoBancoDapper(queryPaginado, queryTotal, 1, 5);

        //Assert
        Assert.NotNull(result);
    }
}
