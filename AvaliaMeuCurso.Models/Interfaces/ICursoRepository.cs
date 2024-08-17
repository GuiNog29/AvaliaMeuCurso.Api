using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Domain.Interfaces
{
    public interface ICursoRepository
    {
        Task<Curso> CriarNovoCurso(Curso curso);
        Task<bool> AtualizarCurso(Curso curso);
        Task<Curso> BuscarCursoPorId(int cursoId);
        Task<bool> ExcluirCurso(int cursoId);
        Task<IEnumerable<Curso>> BuscarTodosCursos();
    }
}
