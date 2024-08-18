using AvaliaMeuCurso.Application.Models.Avaliacao;

namespace AvaliaMeuCurso.Application.Models.Curso
{
    public class CursoComAvaliacoesModel
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<AvaliacaoDetalhesModel>? Avaliacoes { get; set; }
    }
}
