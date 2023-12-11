using projeto.domain.Dtos;

namespace projeto.brasilapiAdapter;
public interface ICepAdapter
{
    Task<CepDTO> BuscarEndereco(string cep);
}
