using AutoMapper;
using AvaliaMeuCurso.Application.Models.Avaliacao;
using AvaliaMeuCurso.Application.Models.Curso;
using AvaliaMeuCurso.Application.Models.Estudante;
using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoModel>().ReverseMap();
            CreateMap<Curso, CursoAtualizacaoModel>().ReverseMap();

            CreateMap<Estudante, EstudanteModel>().ReverseMap();
            CreateMap<Estudante, EstudanteAtualizacaoModel>().ReverseMap();

            CreateMap<Avaliacao, AvaliacaoModel>().ReverseMap();
            CreateMap<Avaliacao, AvaliacaoAtualizacaoModel>().ReverseMap();
        }
    }
}
