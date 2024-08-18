using AvaliaMeuCurso.Application.Models.Curso;

namespace AvaliaMeuCurso.Application.Interfaces.Service
{
    public interface ICursoService
    {
        Task<CursoModel> CriarNovoCurso(CursoModel cursoModel);
        Task<bool> AtualizarCurso(CursoAtualizacaoModel cursoModel, int cursoId);
        Task<CursoModel> BuscarCursoPorId(int cursoId);
        Task<bool> ExcluirCurso(int cursoId);
        Task<IEnumerable<CursoModel>> BuscarTodosCursos();
        Task<IEnumerable<CursoComAvaliacoesModel>> BuscarTodosCursosComAvaliacoes();
    }
}
