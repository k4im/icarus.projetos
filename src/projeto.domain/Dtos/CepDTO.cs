namespace projeto.domain.Dtos;
public record CepDTO
{
    public string Cep { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string Street { get; set; }
}
