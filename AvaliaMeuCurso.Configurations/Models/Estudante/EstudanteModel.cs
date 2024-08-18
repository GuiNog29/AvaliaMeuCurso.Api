using Swashbuckle.AspNetCore.Annotations;
using AvaliaMeuCurso.Application.Interfaces.Dados;

namespace AvaliaMeuCurso.Application.Models.Estudante
{
    public class EstudanteModel : IEstudanteDados
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
