namespace AvaliaMeuCurso.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Avaliacao>? Avaliacoes { get; set; }
    }
}
