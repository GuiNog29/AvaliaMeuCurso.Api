using AutoMapper;
using AvaliaMeuCurso.Models;
using AvaliaMeuCurso.Domain.Interfaces;
using AvaliaMeuCurso.Application.Models.Curso;
using AvaliaMeuCurso.Application.Interfaces.Service;

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
            ValidarDadosCurso(cursoModel.Nome);
            var novoCurso = await _cursoRepository.CriarNovoCurso(_mapper.Map<Curso>(cursoModel));
            if (novoCurso == null)
                throw new Exception("Ocorreu um erro ao criar um novo curso.");

            return _mapper.Map<CursoModel>(novoCurso);
        }

        public async Task<bool> AtualizarCurso(CursoAtualizacaoModel cursoModel, int cursoId)
        {
            await BuscarCursoPorId(cursoId); 
            var atualizouCurso = await _cursoRepository.AtualizarCurso(_mapper.Map<Curso>(cursoModel), cursoId);
            if (!atualizouCurso)
                throw new Exception($"Ocorreu um erro ao atualizar o curso com Id:{cursoId}.");

            return atualizouCurso;
        }

        public async Task<CursoModel> BuscarCursoPorId(int cursoId)
        {
            var curso = await _cursoRepository.BuscarCursoPorId(cursoId);
            if (curso == null)
                throw new CursoNotFoundException(cursoId);

            return _mapper.Map<CursoModel>(curso);
        }

        public async Task<bool> ExcluirCurso(int cursoId)
        {
            await BuscarCursoPorId(cursoId);
            var excluiuCurso = await _cursoRepository.ExcluirCurso(cursoId);
            if (!excluiuCurso)
                throw new Exception($"Ocorreu um erro ao excluir o curso com Id:{cursoId}.");

            return excluiuCurso;
        }

        public async Task<IEnumerable<CursoModel>> BuscarTodosCursos()
        {
            var listaCursos = await BuscarValidarCursos();
            return _mapper.Map<IEnumerable<CursoModel>>(listaCursos);
        }

        public async Task<IEnumerable<CursoComAvaliacoesModel>> BuscarTodosCursosComAvaliacoes()
        {
            var listaCursos = await BuscarValidarCursos();
            return _mapper.Map<IEnumerable<CursoComAvaliacoesModel>>(listaCursos);
        }

        private void ValidarDadosCurso(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException(nameof(nome), "Preencha o nome do curso.");
        }

        private async Task<IEnumerable<Curso>> BuscarValidarCursos()
        {
            var listaCursos = await _cursoRepository.BuscarTodosCursos();
            if (listaCursos == null || !listaCursos.Any())
                throw new Exception("Não tem nenhum curso cadastrado.");

            return listaCursos;
        }
    }

    public class CursoNotFoundException : Exception
    {
        public CursoNotFoundException(int cursoId)
            : base($"Não foi localizado o curso com Id:{cursoId}.") { }
    }
}
