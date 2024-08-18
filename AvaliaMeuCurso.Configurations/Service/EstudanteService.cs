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

        private const bool VALIDAR_EMAIL = true;

        public EstudanteService(IMapper mapper, IEstudanteRepository estudanteRepository)
        {
            _mapper = mapper;
            _estudanteRepository = estudanteRepository;
        }

        public async Task<EstudanteModel> CriarNovoEstudante(EstudanteModel estudanteModel)
        {
            await ValidarDadosEstudante(estudanteModel, VALIDAR_EMAIL);

            var novoEstudante = await _estudanteRepository.CriarNovoEstudante(_mapper.Map<Estudante>(estudanteModel));
            if (novoEstudante == null)
                throw new EstudanteServiceException("Ocorreu um erro ao criar um novo estudante.");

            return _mapper.Map<EstudanteModel>(novoEstudante);
        }

        public async Task<bool> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId)
        {
            var estudanteExistente = await VerificarEstudanteExistente(estudanteId);
            
            var mesmoEmail = estudanteExistente.Email == estudanteModel.Email;

            await ValidarDadosEstudante(estudanteModel, !mesmoEmail);

            var estudanteAtualizado = await _estudanteRepository.AtualizarEstudante(_mapper.Map<Estudante>(estudanteModel), estudanteId);
            if (!estudanteAtualizado)
                throw new EstudanteServiceException($"Ocorreu um erro ao atualizar o estudante com Id:{estudanteId}.");

            return estudanteAtualizado;
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
            await VerificarEstudanteExistente(estudanteId);

            var excluiuEstudante = await _estudanteRepository.ExcluirEstudante(estudanteId);
            if (!excluiuEstudante)
                throw new EstudanteServiceException($"Ocorreu um erro ao excluir o estudante com Id:{estudanteId}.");

            return excluiuEstudante;
        }

        public async Task<IEnumerable<EstudanteModel>> BuscarTodosEstudantes()
        {
            var listaEstudantes = await BuscarEstudantesValidos();
            return _mapper.Map<IEnumerable<EstudanteModel>>(listaEstudantes);
        }

        private async Task ValidarDadosEstudante(IEstudanteDados estudanteDados, bool acaoValidarEmail)
        {
            if (string.IsNullOrEmpty(estudanteDados.Nome))
                throw new EstudanteServiceException("O nome do estudante é obrigatório e não pode estar vazio.");

            if (!ValidadorEmail.EmailValido(estudanteDados.Email))
                throw new EstudanteServiceException("Formato de e-mail incorreto, preencha corretamente.");

            if (acaoValidarEmail)
            {
                var estudanteComMesmoEmail = await _estudanteRepository.BuscarEstudantePorEmail(estudanteDados.Email);
                if (estudanteComMesmoEmail != null)
                    throw new EstudanteServiceException("Existe um estudante com o mesmo e-mail, use um e-mail diferente.");
            }
        }

        private async Task<Estudante> VerificarEstudanteExistente(int estudanteId)
        {
            var estudante = await _estudanteRepository.BuscarEstudantePorId(estudanteId);
            if (estudante == null)
                throw new EstudanteNotFoundException(estudanteId);

            return estudante;
        }

        private async Task<IEnumerable<Estudante>> BuscarEstudantesValidos()
        {
            var listaEstudantes = await _estudanteRepository.BuscarTodosEstudantes();
            if (listaEstudantes == null || !listaEstudantes.Any())
                throw new EstudanteServiceException("Não há nenhum estudante cadastrado.");

            return listaEstudantes;
        }
    }

    public class EstudanteNotFoundException : Exception
    {
        public EstudanteNotFoundException(int estudanteId)
            : base($"Não foi localizado o estudante com Id:{estudanteId}.") { }
    }

    public class EstudanteServiceException : Exception
    {
        public EstudanteServiceException(string message)
            : base(message) { }
    }
}
