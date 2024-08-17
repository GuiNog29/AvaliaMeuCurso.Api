using Swashbuckle.AspNetCore.Annotations;

namespace AvaliaMeuCurso.Models
{
    public class CursoModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<AvaliacaoModel>? Avaliacoes { get; set; }
    }
}
