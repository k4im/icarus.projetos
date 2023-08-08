
using AutoMapper;

namespace projeto.infra.MapperProfile
{

    public class ProfileMapper : Profile
    {
        IRepoProdutosDisponiveis _repo;
        public ProfileMapper(IRepoProdutosDisponiveis repo)
        {
            _repo = repo;
            CreateMap<ProjetoDTO, Projeto>()
                .ForMember(db => db.ProdutoUtilizado, dto => dto.MapFrom(src => _repo.BuscarPorId(src.ProdutoUtilizadoId)))
                .ForMember(db => db.ProdutoUtilizadoId, dto => dto.MapFrom(src => src.ProdutoUtilizadoId));
        }
    }
}