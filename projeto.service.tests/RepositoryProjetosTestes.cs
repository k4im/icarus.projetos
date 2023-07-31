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

        [Fact]
        public async void deve_retornar_true_ao_adicionar_projeto()
        {
            //Arrange
            var projeto = FakeProjeto.factoryProjeto();
            mockRepo.Setup(repo => repo.CriarProjeto(projeto).Result)
            .Returns(true);

            // Act
            var result = await mockRepo.Object.CriarProjeto(projeto);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void deve_retornar_true_ao_atualizar_projeto()
        {
            //Arrange
            var projetoStatus = FakeStatus.factoryStatus();
            mockRepo.Setup(repo => repo.AtualizarStatus(projetoStatus, 1).Result)
            .Returns(true);

            //Act
            var result = await mockRepo.Object.AtualizarStatus(projetoStatus, 1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void deve_retornar_true_ao_deletar_projeto()
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
        public async void deve_retornar_false_ao_adicionar_projeto()
        {
            // Arrange
            var projeto = FakeProjeto.factoryProjeto();
            mockRepo.Setup(repo => repo.CriarProjeto(projeto).Result)
            .Returns(false);

            // Act
            var result = await mockRepo.Object.CriarProjeto(projeto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void deve_retornar_false_ao_atualizar_projeto()
        {
            //Arrange
            var projetoStatus = FakeStatus.factoryStatus();
            mockRepo.Setup(repo => repo.AtualizarStatus(projetoStatus, 1).Result)
            .Returns(false);

            //Act
            var result = await mockRepo.Object.AtualizarStatus(projetoStatus, 1);

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
}