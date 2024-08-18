using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Models.Error;
using AvaliaMeuCurso.Application.Models.Estudante;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudanteController : ControllerBase
    {
        private readonly IEstudanteService _estudanteService;

        public EstudanteController(IEstudanteService estudanteService)
        {
            _estudanteService = estudanteService;
        }

        [HttpPost("CriarNovoEstudante")]
        public async Task<IActionResult> CriarNovoEstudante(EstudanteModel estudanteModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var estudanteCadastrado = await _estudanteService.CriarNovoEstudante(estudanteModel);
                return Ok(estudanteCadastrado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar cadastrar estudante: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpPut("AtualizarEstudante/{estudanteId}")]
        public async Task<IActionResult> AtualizarEstudante(EstudanteAtualizacaoModel estudanteModel, int estudanteId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var estudanteAtualizado = await _estudanteService.AtualizarEstudante(estudanteModel, estudanteId);
                return Ok(estudanteAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar atualizar estudante: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarEstudantePorId/{estudanteId}")]
        public async Task<IActionResult> BuscarEstudantePorId(int estudanteId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var estudante = await _estudanteService.BuscarEstudantePorId(estudanteId);
                return Ok(estudante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar buscar estudante por Id: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpDelete("ExcluirEstudante/{estudanteId}")]
        public async Task<IActionResult> ExcluirEstudante(int estudanteId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _estudanteService.ExcluirEstudante(estudanteId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar excluir estudante: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarTodosEstudantes")]
        public async Task<IActionResult> BuscarTodosEstudantes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var listaEstudantes = await _estudanteService.BuscarTodosEstudantes();
                return Ok(listaEstudantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar listar todos os estudantes: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }
    }
}
