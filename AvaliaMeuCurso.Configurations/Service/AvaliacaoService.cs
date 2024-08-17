using AutoMapper;
using AvaliaMeuCurso.Application.Interfaces;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Models;

namespace AvaliaMeuCurso.Application.Service
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMapper _mapper;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<AvaliacaoModel> CriarNovaAvaliacao(AvaliacaoModel avaliacaoModel)
        {
            var novaAvaliacao = await _avaliacaoRepository.CriarNovaAvaliacao(_mapper.Map<Avaliacao>(avaliacaoModel));
            if (novaAvaliacao == null)
                throw new Exception("Ocorreu um erro ao criar uma nova avaliação.");

            return _mapper.Map<AvaliacaoModel>(novaAvaliacao);
        }

        public async Task<bool> AtualizarAvaliacao(AvaliacaoModel avaliacaoModel)
        {
            var atualizouAvaliacao = await _avaliacaoRepository.AtualizarAvaliacao(_mapper.Map<Avaliacao>(avaliacaoModel));
            if(!atualizouAvaliacao)
                throw new Exception($"Ocorreu um erro ao buscar avaliação com Id:{avaliacaoModel.Id}.");

            return atualizouAvaliacao;
        }

        public async Task<AvaliacaoModel> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            return _mapper.Map<AvaliacaoModel>(await BuscarValidarAvaliacaoPorId(avaliacaoId));
        }

        public async Task<bool> ExcluirAvaliacao(int avaliacaoId)
        {
            await BuscarValidarAvaliacaoPorId(avaliacaoId);
            var excluiuAvaliacao = await _avaliacaoRepository.ExcluirAvaliacao(avaliacaoId);
            if (!excluiuAvaliacao)
                throw new Exception($"Ocorreu um erro ao excluir avaliação com Id:{avaliacaoId}.");

            return excluiuAvaliacao;
        }

        public async Task<IEnumerable<AvaliacaoModel>> BuscarTodasAvaliacoes()
        {
            var listaAvaliacoes = await _avaliacaoRepository.BuscarTodasAvaliacoes();
            if (listaAvaliacoes == null || !listaAvaliacoes.Any())
                throw new Exception($"Não tem nenhuma avaliação cadastrada.");

            return _mapper.Map<IEnumerable<AvaliacaoModel>>(listaAvaliacoes);
        }

        private async Task<Avaliacao> BuscarValidarAvaliacaoPorId(int avaliacaoId)
        {
            var avaliacao = await _avaliacaoRepository.BuscarAvaliacaoPorId(avaliacaoId);
            if (avaliacao == null)
                throw new Exception($"Não foi localizado a avaliação com Id:{avaliacaoId}.");

            return avaliacao;
        }
    }
}
