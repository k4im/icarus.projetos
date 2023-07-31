namespace projeto.service.tests
{
    public class RepositoryProjetosTestes
    {
        Mock<IRepoProjetos> mockRepo = new Mock<IRepoProjetos>();
        [Fact]
        public async void deve_retornar_lista_de_projetos()
        {
            //Arrange
            var resposta = new Response<Projeto>(FakeProjeto.factoryListaProjetos(), 1, 1);
            mockRepo.Setup(repo => repo.BuscarProdutos(1, 5).Result)
            .Returns(resposta);
            //Act
            var result = await mockRepo.Object.BuscarProdutos(1, 5);

            //Assert
            Assert.Equal(result, resposta);
        }

        [Fact]
        public async void deve_retonar_projeto()
        {
            // Arrange
            var resposta = FakeProjeto.factoryProjeto();
            mockRepo.Setup(repo => repo.BuscarPorId(1).Result)
            .Returns(resposta);

            //Act
            var result = await mockRepo.Object.BuscarPorId(1);

            //Assert
            Assert.Equal(result, resposta);
        }
    }
}