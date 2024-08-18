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
            CreateMap<Curso, CursoComAvaliacoesModel>()
                .ForMember(x => x.Avaliacoes, opt => opt.MapFrom(src => src.Avaliacoes));

            CreateMap<Estudante, EstudanteModel>().ReverseMap();
            CreateMap<Estudante, EstudanteAtualizacaoModel>().ReverseMap();

            CreateMap<Avaliacao, AvaliacaoModel>().ReverseMap();
            CreateMap<Avaliacao, AvaliacaoDetalhesModel>()
                .ForMember(x => x.NomeEstudante, opt => opt.MapFrom(src => src.Estudante.Nome))
                .ForMember(x => x.EstudanteId, opt => opt.MapFrom(src => src.Estudante.Id))
                .ForMember(x => x.CursoId, opt => opt.MapFrom(src => src.Curso.Id)) 
                .ForMember(x => x.DataHora, opt => opt.MapFrom(src => src.DataHora.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<Avaliacao, AvaliacaoAtualizacaoModel>().ReverseMap();
        }
    }
}
