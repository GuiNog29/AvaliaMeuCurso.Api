using AutoMapper;
using AvaliaMeuCurso.Models;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Interfaces;

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
            var novoEstudante = await _estudanteRepository.CriarNovoEstudante(_mapper.Map<Estudante>(estudanteModel));
            if (novoEstudante == null)
                throw new Exception("Ocorreu um erro ao criar uma novo estudante.");

            return _mapper.Map<EstudanteModel>(novoEstudante);
        }

        public async Task<bool> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId)
        {
            await BuscarValidarEstudantePorId(estudanteId);
            var atualizouEstudante = await _estudanteRepository.AtualizarEstudante(_mapper.Map<Estudante>(estudanteModel), estudanteId);
            if (!atualizouEstudante)
                throw new Exception($"Ocorreu um erro ao atualizar estudante com Id:{estudanteId}.");

            return atualizouEstudante;
        }

        public async Task<EstudanteModel> BuscarEstudantePorId(int estudanteId)
        {
            return _mapper.Map<EstudanteModel>(await BuscarValidarEstudantePorId(estudanteId));
        }

        public async Task<bool> ExcluirEstudante(int estudanteId)
        {
            await BuscarValidarEstudantePorId(estudanteId);
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

        private async Task<Estudante> BuscarValidarEstudantePorId(int estudanteId)
        {
            var estudante = await _estudanteRepository.BuscarEstudantePorId(estudanteId);
            if (estudante == null)
                throw new Exception($"Não foi localizado o estudante com Id:{estudanteId}.");

            return estudante;
        }
    }
}
