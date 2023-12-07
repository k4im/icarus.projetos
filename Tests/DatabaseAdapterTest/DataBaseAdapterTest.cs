namespace DatabaseAdapterTest;

public class DataBaseAdapterTest
{
    IDatabaseAdapter Adapter = new DatabaseAdapter(new DatabaseConnectionSQLITE());
    
    [Fact]
    public async void Deve_realizar_operacao_select_no_banco()
    {
        //Arrange
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
            @resultado
        OFFSET 
            @pagina";
        var queryTotal = "SELECT count(*) FROM Projetos";

        //Act 
        var result = await this.Adapter.PaginarProjetosDoBancoDapper(queryPaginado, queryTotal, 1, 5);

        //Assert
        Assert.NotNull(result);
    }
    [Fact]
    public void Deve_realizar_filtragem_ao_banco_de_dados()
    {
        //Arrange
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
        WHERE
            Status 
        LIKE
            @filter   
        LIMIT 
            @resultado 
        OFFSET 
            @pagina";
        var queryTotal = "SELECT COUNT(*) FROM Projetos WHERE Status LIKE @filter OR Nome LIKE @filter";
        
        // Act
        var result = this.Adapter.FiltrarDadosPaginadosDoBancoDapper(queryPaginado, "teste", queryTotal, 1, 5);
        
        // Then
        Assert.NotNull(result);
    }

    [Fact]
    public void Deve_buscar_dados_pelo_id()
    {
        // Given
        var query = @"
            SELECT 
                Projetos.*,
                ProdutosEmEstoque.*
            FROM 
                Projetos 
            INNER JOIN 
                ProdutosEmEstoque
            ON 
                Projetos.ProdutoUtilizadoId = ProdutosEmEstoque.Id
            WHERE 
                Projetos.Id 
            LIKE 
                @busca";
        // When
         var result = this.Adapter.BuscarDadosDoBancoPeloId(1, query);
        
        // Then
        Assert.NotNull(result);
    }
}