using Swashbuckle.AspNetCore.Annotations;

namespace AvaliaMeuCurso.Application.Models.Curso
{
    public class CursoModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
