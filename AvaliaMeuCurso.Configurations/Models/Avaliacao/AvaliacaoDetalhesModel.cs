namespace AvaliaMeuCurso.Application.Models.Avaliacao
{
    public class AvaliacaoDetalhesModel
    {
        public int Id { get; set; }
        public int Estrelas { get; set; }
        public string? Comentario { get; set; }
        public required string DataHora { get; set; }
        public int CursoId { get; set; }
        public required string NomeEstudante { get; set; }
        public int EstudanteId { get; set; }
    }
}
