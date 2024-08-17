using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Application.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<AvaliacaoModel> CriarNovaAvaliacao(AvaliacaoModel avaliacaoModel);
        Task<bool> AtualizarAvaliacao(AvaliacaoModel avaliacaoModel);
        Task<AvaliacaoModel> BuscarAvaliacaoPorId(int avaliacaoId);
        Task<bool> ExcluirAvaliacao(int avaliacaoId);
        Task<IEnumerable<AvaliacaoModel>> BuscarTodasAvaliacoes();
    }
}
