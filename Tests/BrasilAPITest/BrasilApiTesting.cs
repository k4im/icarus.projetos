using Microsoft.Extensions.Configuration;
using projeto.brasilapiAdapter;
using projeto.domain.Dtos;

namespace BrasilAPITest;

public class BrasilApiTesting
{
    [Fact]
    public async void Efetuar_chamada_get()
    {
        //Arrange
        var cepAdapter = new CepAdapter();
        var expect = new CepDTO {
            Cep = "88525810",
            State = "SC",
            City = "Lages",
            Neighborhood = "Penha",
            Street = "Rua Manoel Antunes de Mello"
        };

        //Act
        var result = await cepAdapter.BuscarEndereco("88525810");
        
        //Assert
        Assert.Equal(result, expect);
    }
}