using Swashbuckle.AspNetCore.Annotations;

namespace AvaliaMeuCurso.Models
{
    public class EstudanteModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
