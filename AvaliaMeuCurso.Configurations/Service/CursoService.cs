using AutoMapper;
using AvaliaMeuCurso.Models;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Interfaces;

namespace AvaliaMeuCurso.Application.Service
{
    public class CursoService : ICursoService
    {
        private readonly IMapper _mapper;
        private readonly ICursoRepository _cursoRepository;

        public CursoService(IMapper mapper, ICursoRepository cursoRepository)
        {
            _mapper = mapper;
            _cursoRepository = cursoRepository;
        }

        public async Task<CursoModel> CriarNovoCurso(CursoModel cursoModel)
        {
            var novoCurso = await _cursoRepository.CriarNovoCurso(_mapper.Map<Curso>(cursoModel));
            if (novoCurso == null)
                throw new Exception("Ocorreu um erro ao criar um novo Curso.");

            return _mapper.Map<CursoModel>(novoCurso);
        }

        public async Task<bool> AtualizarCurso(CursoAtualizacaoModel cursoModel, int cursoId)
        {
            await BuscarValidarCursoPorId(cursoId);
            var atualizouCurso = await _cursoRepository.AtualizarCurso(_mapper.Map<Curso>(cursoModel), cursoId);
            if (!atualizouCurso)
                throw new Exception($"Ocorreu um erro ao atualizar curso com Id:{cursoId}.");

            return atualizouCurso;
        }

        public async Task<CursoModel> BuscarCursoPorId(int cursoId)
        {
            return _mapper.Map<CursoModel>(await BuscarValidarCursoPorId(cursoId));
        }

        public async Task<bool> ExcluirCurso(int cursoId)
        {
            await BuscarValidarCursoPorId(cursoId);
            var excluiucurso = await _cursoRepository.ExcluirCurso(cursoId);
            if (!excluiucurso)
                throw new Exception($"Ocorreu um erro ao excluir curso com Id:{cursoId}.");

            return excluiucurso;
        }

        public async Task<IEnumerable<CursoModel>> BuscarTodosCursos()
        {
            var listaCursos = await _cursoRepository.BuscarTodosCursos();
            if (listaCursos == null || !listaCursos.Any())
                throw new Exception($"Não tem nenhum curso cadastrado.");

            return _mapper.Map<IEnumerable<CursoModel>>(listaCursos);
        }

        private async Task<Curso> BuscarValidarCursoPorId(int cursoId)
        {
            var curso = await _cursoRepository.BuscarCursoPorId(cursoId);
            if (curso == null)
                throw new Exception($"Não foi localizado o curso com Id:{cursoId}.");

            return curso;
        }
    }
}
