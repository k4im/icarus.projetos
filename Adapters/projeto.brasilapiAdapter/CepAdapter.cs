namespace projeto.brasilapiAdapter;
public class CepAdapter : ICepAdapter
{      
    public async Task<CepDTO> BuscarEndereco(string cep)
    {
        /*
        Tentar validar futuramente a utilizacao de variavel de ambientes
        Desta forma será possivel estar repassando a uri através de uma variavel.
        nao deixando */
        
        //var uri = _config["URL_BRASILAPI"].ToString();
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
        var result = await client.GetStringAsync($"https://brasilapi.com.br/api/cep/v1/{cep}");        
        if(result != null ) return JsonConvert.DeserializeObject<CepDTO>(result);
        return new CepDTO();
    }
}
