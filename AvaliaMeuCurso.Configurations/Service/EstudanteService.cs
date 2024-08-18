using AutoMapper;
using AvaliaMeuCurso.Models;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Helpers;
using AvaliaMeuCurso.Application.Models.Estudante;
using AvaliaMeuCurso.Application.Interfaces.Service;
using AvaliaMeuCurso.Application.Interfaces.Dados;

namespace AvaliaMeuCurso.Application.Service
{
    public class EstudanteService : IEstudanteService
    {
        private readonly IMapper _mapper;
        private readonly IEstudanteRepository _estudanteRepository;

        public EstudanteService(IMapper mapper, IEstudanteRepository estudanteRepository)
        {
            _mapper = mapper;
            _estudanteRepository = estudanteRepository;
        }

        public async Task<EstudanteModel> CriarNovoEstudante(EstudanteModel estudanteModel)
        {
            await ValidarDadosEstudante(estudanteModel);
            var novoEstudante = await _estudanteRepository.CriarNovoEstudante(_mapper.Map<Estudante>(estudanteModel));
            if (novoEstudante == null)
                throw new Exception("Ocorreu um erro ao criar uma novo estudante.");

            return _mapper.Map<EstudanteModel>(novoEstudante);
        }

        public async Task<bool> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId)
        {
            await ValidarDadosEstudante(estudanteModel);
            await BuscarEstudantePorId(estudanteId);
            var atualizouEstudante = await _estudanteRepository.AtualizarEstudante(_mapper.Map<Estudante>(estudanteModel), estudanteId);
            if (!atualizouEstudante)
                throw new Exception($"Ocorreu um erro ao atualizar estudante com Id:{estudanteId}.");

            return atualizouEstudante;
        }

        public async Task<EstudanteModel> BuscarEstudantePorId(int estudanteId)
        {
            var estudante = await _estudanteRepository.BuscarEstudantePorId(estudanteId);
            if (estudante == null)
                throw new EstudanteNotFoundException(estudanteId);

            return _mapper.Map<EstudanteModel>(estudante);
        }

        public async Task<bool> ExcluirEstudante(int estudanteId)
        {
            await BuscarEstudantePorId(estudanteId);
            var excluiuEstudante = await _estudanteRepository.ExcluirEstudante(estudanteId);
            if (!excluiuEstudante)
                throw new Exception($"Ocorreu um erro ao excluir estudante com Id:{estudanteId}.");

            return excluiuEstudante;
        }

        public async Task<IEnumerable<EstudanteModel>> BuscarTodosEstudantes()
        {
            var listaEstudantes = await _estudanteRepository.BuscarTodosEstudantes();
            if (listaEstudantes == null || !listaEstudantes.Any())
                throw new Exception($"Não tem nenhum estudante cadastrado.");

            return _mapper.Map<IEnumerable<EstudanteModel>>(listaEstudantes);
        }

        private async Task ValidarDadosEstudante(IEstudanteDados estudanteDados)
        {
            if (string.IsNullOrEmpty(estudanteDados.Nome))
                throw new ArgumentNullException(nameof(estudanteDados.Nome), "Preencha o nome do estudante.");

            if (!ValidadorEmail.EmailValido(estudanteDados.Email))
                throw new ArgumentException("Formato de e-mail incorreto, preencha corretamente.", nameof(estudanteDados.Email));
        }
    }

    public class EstudanteNotFoundException : Exception
    {
        public EstudanteNotFoundException(int estudanteId)
            : base($"Não foi localizado o estudante com Id:{estudanteId}.") { }
    }
}
