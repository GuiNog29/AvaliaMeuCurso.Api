using AvaliaMeuCurso.Application.Interfaces.Dados;

namespace AvaliaMeuCurso.Application.Models.Estudante
{
    public class EstudanteAtualizacaoModel : IEstudanteDados
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
