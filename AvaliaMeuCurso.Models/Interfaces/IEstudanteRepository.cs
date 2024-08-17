using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Domain.Interfaces
{
    public interface IEstudanteRepository
    {
        Task<Estudante> CriarNovoEstudante(Estudante estudante);
        Task<bool> AtualizarEstudante(Estudante estudante);
        Task<Estudante> BuscarEstudantePorId(int estudanteId);
        Task<bool> ExcluirEstudante(int estudanteId);
        Task<IEnumerable<Estudante>> BuscarTodosEstudantes();
    }
}
