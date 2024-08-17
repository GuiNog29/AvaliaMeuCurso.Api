using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Application.Interfaces
{
    public interface IEstudanteService
    {
        Task<EstudanteModel> CriarNovoEstudante(EstudanteModel estudanteModel);
        Task<bool> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId);
        Task<EstudanteModel> BuscarEstudantePorId(int estudanteId);
        Task<bool> ExcluirEstudante(int estudanteId);
        Task<IEnumerable<EstudanteModel>> BuscarTodosEstudantes();
    }
}
