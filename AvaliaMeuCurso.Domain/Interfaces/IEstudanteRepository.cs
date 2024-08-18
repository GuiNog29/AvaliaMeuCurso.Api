using AvaliaMeuCurso.Domain.Entities;

namespace AvaliaMeuCurso.Domain.Interfaces
{
    public interface IEstudanteRepository
    {
        Task<Estudante> CriarNovoEstudante(Estudante estudante);
        Task<bool> AtualizarEstudante(Estudante estudante, int estudanteId);
        Task<Estudante> BuscarEstudantePorId(int estudanteId);
        Task<Estudante> BuscarEstudantePorEmail(string email);
        Task<bool> ExcluirEstudante(int estudanteId);
        Task<IEnumerable<Estudante>> BuscarTodosEstudantes();
    }
}
