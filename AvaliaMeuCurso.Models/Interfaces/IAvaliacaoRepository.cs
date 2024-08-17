using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<Avaliacao> CriarNovaAvaliacao(Avaliacao avaliacao);
        Task<bool> AtualizarAvaliacao(Avaliacao avaliacao, int avaliacaoId);
        Task<Avaliacao> BuscarAvaliacaoPorId(int avaliacaoId);
        Task<bool> ExcluirAvaliacao(int avaliacaoId);
        Task<IEnumerable<Avaliacao>> BuscarTodasAvaliacoes();
    }
}
