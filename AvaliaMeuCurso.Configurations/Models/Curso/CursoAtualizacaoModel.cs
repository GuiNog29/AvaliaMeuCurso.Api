using AvaliaMeuCurso.Application.Models.Avaliacao;

namespace AvaliaMeuCurso.Application.Models.Curso
{
    public class CursoAtualizacaoModel
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<AvaliacaoModel>? Avaliacoes { get; set; }
    }
}
