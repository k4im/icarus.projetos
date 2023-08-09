using AutoMapper;
namespace projeto.service.Mapper;
public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<ProjetoDTO, Projeto>()
            .ForMember(db => db.ProdutoUtilizado, dto => dto.MapFrom(src => RepoProdutosDisponiveis.BuscarPorId(src.ProdutoUtilizadoId)))
            .ForMember(db => db.ProdutoUtilizadoId, dto => dto.MapFrom(src => src.ProdutoUtilizadoId));
    }
}
