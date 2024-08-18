using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Helpers;
using AvaliaMeuCurso.Application.Models.Curso;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IValidadorErro _validadorErro;

        public CursoController(ICursoService cursoService, IValidadorErro validadorErro)
        {
            _cursoService = cursoService;
            _validadorErro = validadorErro;
        }

        [HttpPost("CriarNovoCurso")]
        public async Task<ActionResult<CursoModel>> CriarNovoCurso(CursoModel cursoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cursoCadastrado = await _cursoService.CriarNovoCurso(cursoModel);
                return Ok(cursoCadastrado);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("cadastrar curso", ex);
            }
        }

        [HttpPut("AtualizarCurso/{cursoId}")]
        public async Task<IActionResult> AtualizarCurso(CursoAtualizacaoModel cursoModel, int cursoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cursoAtualizado = await _cursoService.AtualizarCurso(cursoModel, cursoId);
                return Ok(cursoAtualizado);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("atualizar curso", ex);
            }
        }

        [HttpGet("BuscarCursoPorId/{cursoId}")]
        public async Task<ActionResult<CursoModel>> BuscarCursoPorId(int cursoId)
        {
            try
            {
                var curso = await _cursoService.BuscarCursoPorId(cursoId);
                if (curso == null)
                    return NotFound($"Curso com Id:{cursoId} não encontrado.");

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("buscar curso por Id", ex);
            }
        }

        [HttpDelete("ExcluirCurso/{cursoId}")]
        public async Task<IActionResult> ExcluirCurso(int cursoId)
        {
            try
            {
                var excluiu = await _cursoService.ExcluirCurso(cursoId);
                return Ok(excluiu);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("excluir curso", ex);
            }
        }

        [HttpGet("BuscarTodosCursos")]
        public async Task<ActionResult<IEnumerable<CursoModel>>> BuscarTodosCursos()
        {
            try
            {
                var listaCursos = await _cursoService.BuscarTodosCursos();
                return Ok(listaCursos);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("listar todos os cursos", ex);
            }
        }

        [HttpGet("BuscarTodosCursosComAvaliacoes")]
        public async Task<ActionResult<IEnumerable<CursoModel>>> BuscarTodosCursosComAvaliacoes()
        {
            try
            {
                var listaCursos = await _cursoService.BuscarTodosCursosComAvaliacoes();
                return Ok(listaCursos);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("listar todos os cursos com suas avaliações", ex);
            }
        }
    }
}
