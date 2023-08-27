namespace projeto.service.tests;

public class ProjetoCreationTest
{


    [Fact]
    public void Deve_falhar_valor_ao_criar_projeto()
    {
        // Arrange
        Action act = () => FakeProjeto.FactoryProjetoValMenorQueZero();

        // Action
        var result = Assert.Throws<Exception>(act);

        // Assert
        Assert.Equal("O valor não pode ser negativo!", result.Message);
    }

    [Fact]
    public void Deve_criar_projeto()
    {
        // Arrange
        var projeto = FakeProjeto.FactoryProjeto();

        // Assert
        Assert.NotNull(projeto);
    }
}
