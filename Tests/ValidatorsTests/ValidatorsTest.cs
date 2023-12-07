namespace ValidatorsTests;

public class ValidatorInputTest
{
    [Fact]
    public void Deve_retornar_false_no_validador_menor_que_zero()
    {
        //Arrange
        var ValorMenorQueZero = -1;
        var ValorIgualAZero = 0;
        var ValorMaiorQueZero = 1;
    
        // Act
        var ResultMenorQueZero = ValidadorDeInput.ValidarMenorDoQueZero(ValorMenorQueZero);
        var ResultIgualAZero = ValidadorDeInput.ValidarMenorDoQueZero(ValorIgualAZero);
        var ResultMaiorQueZero = ValidadorDeInput.ValidarMenorDoQueZero(ValorMaiorQueZero);

        //Assert
        Assert.True(ResultMenorQueZero);
        Assert.False(ResultIgualAZero);
        Assert.False(ResultMaiorQueZero);
    }

    [Fact]
    public void Deve_retornar_false_validador_regex()
    {
        // Arrange
        var valor = "Teste de Regex Onde é Enviado Valores 4ltet0ri05";
        
        // Acts
        var result = ValidadorDeInput.ValidarInputRegex(valor);
        
        // Assert

        Assert.False(result);
    }

    [Fact]
    public void Deve_retornar_true_validador_regex()
    {
        // Arrange
        var valor = "Teste de Regex@@@ Onde é Enviado Valores 4ltet0ri05";
        
        // Acts
        var result = ValidadorDeInput.ValidarInputRegex(valor);
        
        // Assert
        Assert.True(result);
    }
}
