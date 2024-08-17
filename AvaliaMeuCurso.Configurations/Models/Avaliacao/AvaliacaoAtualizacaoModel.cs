namespace AvaliaMeuCurso.Application.Models.Avaliacao
{
    public class AvaliacaoAtualizacaoModel
    {
        public int Estrelas { get; set; }
        public string? Comentario { get; set; }
        public DateTime DataHora { get; set; }
        public int CursoId { get; set; }
        public int EstudanteId { get; set; }
    }
}
