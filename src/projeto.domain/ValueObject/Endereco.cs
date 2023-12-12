using projeto.brasilapiAdapter;

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

        var adapter = new CepAdapter();
        var result = await adapter.BuscarEndereco(cepLimpo);
        
        if(result != null) {
            this.Cep = result.Cep;
            this.Estado = result.State;
            this.Cidade = result.City;
            this.Rua = result.Street;
            this.Bairro = result.Neighborhood;
            return cepLimpo;
        }
        if (!Regex.IsMatch(cep, @"^[0-9 ]+$", RegexOptions.Compiled))
            throw new Exception("O cep precisa conter apenas numeros");
        return cepLimpo;
    }

}
