namespace AvaliaMeuCurso.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
    }
}
