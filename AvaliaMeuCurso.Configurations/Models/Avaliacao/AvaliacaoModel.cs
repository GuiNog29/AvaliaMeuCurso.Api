using AvaliaMeuCurso.Application.Interfaces.Dados;
using Swashbuckle.AspNetCore.Annotations;

namespace AvaliaMeuCurso.Application.Models.Avaliacao
{
    public class AvaliacaoModel : IAvaliacaoDados
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public int Estrelas { get; set; }
        public string? Comentario { get; set; }
        public DateTime DataHora { get; set; }
        public int CursoId { get; set; }
        public int EstudanteId { get; set; }
    }
}
