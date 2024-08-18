namespace AvaliaMeuCurso.Domain.Entities
{
    public class Estudante
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
