using AvaliaMeuCurso.Application.Models.Avaliacao;

namespace AvaliaMeuCurso.Application.Interfaces.Service
{
    public interface IAvaliacaoService
    {
        Task<AvaliacaoModel> CriarNovaAvaliacao(AvaliacaoModel avaliacaoModel);
        Task<bool> AtualizarAvaliacao(AvaliacaoAtualizacaoModel avaliacaoModel, int avaliacaoId);
        Task<AvaliacaoModel> BuscarAvaliacaoPorId(int avaliacaoId);
        Task<bool> ExcluirAvaliacao(int avaliacaoId);
        Task<IEnumerable<AvaliacaoModel>> BuscarTodasAvaliacoes();
    }
}
