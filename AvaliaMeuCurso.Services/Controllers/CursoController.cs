using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Models.Curso;
using AvaliaMeuCurso.Application.Models.Error;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpPost("CriarNovoCurso")]
        public async Task<IActionResult> CriarNovoCurso(CursoModel cursoModel)
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
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar cadastrar curso: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
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
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar atualizar curso: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarCursoPorId/{cursoId}")]
        public async Task<IActionResult> BuscarCursoPorId(int cursoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var curso = await _cursoService.BuscarCursoPorId(cursoId);
                return Ok(curso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar buscar curso por Id: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpDelete("ExcluirCurso/{cursoId}")]
        public async Task<IActionResult> ExcluirCurso(int cursoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _cursoService.ExcluirCurso(cursoId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar excluir curso: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarTodosCursos")]
        public async Task<IActionResult> BuscarTodosCursos()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var listaCursos = await _cursoService.BuscarTodosCursos();
                return Ok(listaCursos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar listar todos os cursos: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarTodosCursosComAvaliacoes")]
        public async Task<IActionResult> BuscarTodosCursosComAvaliacoes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var listaCursos = await _cursoService.BuscarTodosCursosComAvaliacoes();
                return Ok(listaCursos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar listar todos os cursos juntos com suas avaliações: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }
    }
}
