using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Interfaces;
using AvaliaMeuCurso.Application.Models.Error;
using AvaliaMeuCurso.Application.Models.Avaliacao;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpPost("CriarNovaAvaliacao")]
        public async Task<IActionResult> CriarNovaAvaliacao(AvaliacaoModel avaliacaoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var avaliacaoCadastrada = await _avaliacaoService.CriarNovaAvaliacao(avaliacaoModel);
                return Ok(avaliacaoCadastrada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar cadastrar avaliação: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpPut("AtualizarAvaliacao/{avaliacaoId}")]
        public async Task<IActionResult> AtualizarAvaliacao(AvaliacaoAtualizacaoModel avaliacaoModel, int avaliacaoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var avaliacaoAtualizada = await _avaliacaoService.AtualizarAvaliacao(avaliacaoModel, avaliacaoId);
                return Ok(avaliacaoAtualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar atualizar avaliação: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarAvaliacaoPorId/{avaliacaoId}")]
        public async Task<IActionResult> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var avaliacao = await _avaliacaoService.BuscarAvaliacaoPorId(avaliacaoId);
                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar buscar avaliação por Id: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpDelete("ExcluirAvaliacao/{avaliacaoId}")]
        public async Task<IActionResult> ExcluirAvaliacao(int avaliacaoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _avaliacaoService.ExcluirAvaliacao(avaliacaoId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar excluir avaliação: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }

        [HttpGet("BuscarTodasAvaliacoes")]
        public async Task<IActionResult> BuscarTodasAvaliacoes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var listaAvaliacoes = await _avaliacaoService.BuscarTodasAvaliacoes();
                return Ok(listaAvaliacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErroModel
                {
                    Mensagem = $"Ocorreu um erro ao tentar listar todas avaliações: erro {ex.Message}",
                    Detalhes = ex.StackTrace
                });
            }
        }
    }
}
