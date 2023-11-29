namespace projeto.service.tests;

public class RepositoryProjetosTestes
{
    Mock<IRepoProjetos> mockRepo = new Mock<IRepoProjetos>();
    
    [Fact]
    public async void Deve_retornar_true_ao_adicionar_projeto()
    {
        //Arrange
        var projeto = FakeProjeto.FactoryProjeto();
        mockRepo.Setup(repo => repo.CriarProjeto(projeto).Result)
        .Returns(true);

        // Act
        var result = await mockRepo.Object.CriarProjeto(projeto);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void Deve_retornar_true_ao_atualizar_projeto()
    {
        //Arrange
        mockRepo.Setup(repo => repo.AtualizarStatus("projetoStatus", 1).Result)
        .Returns(true);

        //Act
        var result = await mockRepo.Object.AtualizarStatus("projetoStatus", 1);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void Deve_retornar_true_ao_deletar_projeto()
    {
        //Arrange
        mockRepo.Setup(repo => repo.DeletarProjeto(1).Result)
        .Returns(true);

        //Act
        var result = await mockRepo.Object.DeletarProjeto(1);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void Deve_retornar_false_ao_adicionar_projeto()
    {
        // Arrange
        var projeto = FakeProjeto.FactoryProjeto();
        mockRepo.Setup(repo => repo.CriarProjeto(projeto).Result)
        .Returns(false);

        // Act
        var result = await mockRepo.Object.CriarProjeto(projeto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async void Deve_retornar_false_ao_atualizar_projeto()
    {
        //Arrange
        mockRepo.Setup(repo => repo.AtualizarStatus("Teste", 1).Result)
        .Returns(false);

        //Act
        var result = await mockRepo.Object.AtualizarStatus("Teste", 1);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async void deve_retornar_false_ao_deletar_projeto()
    {
        // Arrange
        mockRepo.Setup(repo => repo.DeletarProjeto(1).Result)
        .Returns(false);

        // Act
        var result = await mockRepo.Object.DeletarProjeto(1);

        // Assert
        Assert.False(result);
    }
}
