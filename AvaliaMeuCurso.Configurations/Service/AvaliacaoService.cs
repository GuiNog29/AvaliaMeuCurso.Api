using AutoMapper;
using AvaliaMeuCurso.Models;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Models.Avaliacao;
using AvaliaMeuCurso.Application.Interfaces.Dados;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Application.Service
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IMapper _mapper;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly ICursoService _cursoService;
        private readonly IEstudanteService _estudanteService;

        public AvaliacaoService(IMapper mapper, IAvaliacaoRepository avaliacaoRepository,
                                ICursoService cursoService, IEstudanteService estudanteService)
        {
            _mapper = mapper;
            _avaliacaoRepository = avaliacaoRepository;
            _cursoService = cursoService;
            _estudanteService = estudanteService;
        }

        public async Task<AvaliacaoModel> CriarNovaAvaliacao(AvaliacaoModel avaliacaoModel)
        {
            await ValidarDadosAvaliacao(avaliacaoModel);

            avaliacaoModel.DataHora = DateTime.Now;
            var novaAvaliacao = await _avaliacaoRepository.CriarNovaAvaliacao(_mapper.Map<Avaliacao>(avaliacaoModel));
            if (novaAvaliacao == null)
                throw new AvaliacaoServiceException("Ocorreu um erro ao criar uma nova avaliação.");

            return _mapper.Map<AvaliacaoModel>(novaAvaliacao);
        }

        public async Task<bool> AtualizarAvaliacao(AvaliacaoAtualizacaoModel avaliacaoModel, int avaliacaoId)
        {
            await ValidarEstrelas(avaliacaoModel);

            var avaliacao = await BuscarAvaliacaoPorId(avaliacaoId);
            avaliacaoModel.DataHora = DateTime.Now;
            avaliacaoModel.CursoId = avaliacao.CursoId;
            avaliacaoModel.EstudanteId = avaliacao.EstudanteId;

            var atualizouAvaliacao = await _avaliacaoRepository.AtualizarAvaliacao(_mapper.Map<Avaliacao>(avaliacaoModel), avaliacaoId);
            if (!atualizouAvaliacao)
                throw new AvaliacaoServiceException($"Ocorreu um erro ao atualizar a avaliação com Id:{avaliacaoId}.");

            return atualizouAvaliacao;
        }

        public async Task<AvaliacaoModel> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            var avaliacao = await _avaliacaoRepository.BuscarAvaliacaoPorId(avaliacaoId);
            if (avaliacao == null)
                throw new AvaliacaoNotFoundException(avaliacaoId);

            return _mapper.Map<AvaliacaoModel>(avaliacao);
        }

        public async Task<bool> ExcluirAvaliacao(int avaliacaoId)
        {
            await BuscarAvaliacaoPorId(avaliacaoId);
            var excluiuAvaliacao = await _avaliacaoRepository.ExcluirAvaliacao(avaliacaoId);
            if (!excluiuAvaliacao)
                throw new AvaliacaoServiceException($"Ocorreu um erro ao excluir a avaliação com Id:{avaliacaoId}.");

            return excluiuAvaliacao;
        }

        public async Task<IEnumerable<AvaliacaoModel>> BuscarTodasAvaliacoes()
        {
            var listaAvaliacoes = await _avaliacaoRepository.BuscarTodasAvaliacoes();
            if (listaAvaliacoes == null || !listaAvaliacoes.Any())
                throw new AvaliacaoServiceException("Não tem nenhuma avaliação cadastrada.");

            return _mapper.Map<IEnumerable<AvaliacaoModel>>(listaAvaliacoes);
        }

        private async Task ValidarDadosAvaliacao(AvaliacaoModel avaliacaoModel)
        {
            await ValidarEstrelas(avaliacaoModel);
            await ValidarEstudanteCursoExistem(avaliacaoModel);
        }

        private async Task ValidarEstrelas(IAvaliacaoDados avaliacaoDados)
        {
            if (avaliacaoDados.Estrelas < 1 || avaliacaoDados.Estrelas > 5)
                throw new AvaliacaoServiceException("As estrelas devem ser de 1 até 5.");
        }

        private async Task ValidarEstudanteCursoExistem(AvaliacaoModel avaliacaoModel)
        {
            await _cursoService.BuscarCursoPorId(avaliacaoModel.CursoId);
            await _estudanteService.BuscarEstudantePorId(avaliacaoModel.EstudanteId);
        }
    }

    public class AvaliacaoNotFoundException : Exception
    {
        public AvaliacaoNotFoundException(int avaliacaoId)
            : base($"Não foi localizada a avaliação com Id:{avaliacaoId}.") { }
    }

    public class AvaliacaoServiceException : Exception
    {
        public AvaliacaoServiceException(string message)
            : base(message) { }
    }
}
