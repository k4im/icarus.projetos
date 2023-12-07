namespace projeto.domain;


public class Response<T>
{
    public Response(List<T> data, int paginaAtual, int totalDePaginas, int totalItens)
    {
        Data = data;
        PaginaAtual = paginaAtual;
        TotalDePaginas = totalDePaginas;
        TotalItens = totalItens;
    }

    public List<T> Data { get; } = new List<T>();
    public int PaginaAtual { get; }
    public int TotalDePaginas { get; }
    public int TotalItens { get; }
}
