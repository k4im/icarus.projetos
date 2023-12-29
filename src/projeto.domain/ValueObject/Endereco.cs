namespace projeto.domain.ValueObject;
public class Endereco
{
    public string Cep { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Rua { get; set; }
    public sbyte Numero { get; set; }

    async Task<string> ValidarCep(string cep) {
        var cepLimpo = cep.Trim()
        .Replace(".", "")
        .Replace("-", "");

        if (!Regex.IsMatch(cep, @"^[0-9 ]+$", RegexOptions.Compiled))
            throw new Exception("O cep precisa conter apenas numeros");
        return cepLimpo;
    }

}
