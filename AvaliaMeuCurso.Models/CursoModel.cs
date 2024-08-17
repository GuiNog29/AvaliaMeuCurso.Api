namespace AvaliaMeuCurso.Models
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<AvaliacaoModel> Avaliacoes { get; set; }
    }
}
