using Swashbuckle.AspNetCore.Annotations;
using AvaliaMeuCurso.Application.Interfaces.Dados;

namespace AvaliaMeuCurso.Application.Models.Avaliacao
{
    public class AvaliacaoAtualizacaoModel : IAvaliacaoDados
    {
        public int Estrelas { get; set; }
        public string? Comentario { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public DateTime DataHora { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int CursoId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int EstudanteId { get; set; }
    }
}
