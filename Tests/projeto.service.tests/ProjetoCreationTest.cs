namespace projeto.service.tests;

public class ProjetoCreationTest
{

    [Fact]
    public void Deve_falhar_nome_ao_criar_projeto()
    {
        // Arrange
        Action actRegex = () => FakeProjeto.FactoryProjetoNameError();
        Action actNull = () => FakeProjeto.FactoryProjetoNameErrorNull();

        // Action
        var resultRegex = Assert.Throws<Exception>(actRegex);
        var resultNull = Assert.Throws<Exception>(actNull);
        // Assert
        Assert.Equal("O nome não pode conter caracteres especiais", resultRegex.Message);
        Assert.Equal("Campo não pode ser nulo", resultNull.Message);
    }

    [Fact]
    public void Deve_falhar_descricao_ao_criar_projeto()
    {
        // Arrange
        Action actRegex = () => FakeProjeto.FactoryProjetoDescError();
        Action actNull = () => FakeProjeto.FactoryProjetoDescErrorNull();

        // Action
        var resultRegex = Assert.Throws<Exception>(actRegex);
        var resultNull = Assert.Throws<Exception>(actNull);

        // Assert
        Assert.Equal("A descrição não pode conter caracteres especiais", resultRegex.Message);
        Assert.Equal("Campo não pode ser nulo", resultNull.Message);
    }

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
