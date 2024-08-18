using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Helpers;
using AvaliaMeuCurso.Application.Models.Avaliacao;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IValidadorErro _validadorErro;

        public AvaliacaoController(IAvaliacaoService avaliacaoService, IValidadorErro validadorErro)
        {
            _avaliacaoService = avaliacaoService;
            _validadorErro = validadorErro;
        }

        /// <summary>
        /// Método responsável por criar uma nova alvaliação
        /// </summary>
        /// <param name="avaliacaoModel"></param>
        /// <returns></returns>
        [HttpPost("CriarNovaAvaliacao")]
        public async Task<ActionResult<AvaliacaoModel>> CriarNovaAvaliacao([FromBody] AvaliacaoModel avaliacaoModel)
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
                return _validadorErro.TratarErro("cadastrar avaliação", ex);
            }
        }

        /// <summary>
        /// Método responsável por atualizar avaliação
        /// </summary>
        /// <param name="avaliacaoModel"></param>
        /// <param name="avaliacaoId"></param>
        /// <returns></returns>
        [HttpPut("AtualizarAvaliacao/{avaliacaoId}")]
        public async Task<IActionResult> AtualizarAvaliacao([FromBody] AvaliacaoAtualizacaoModel avaliacaoModel, int avaliacaoId)
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
                return _validadorErro.TratarErro("atualizar avaliação", ex);
            }
        }

        /// <summary>
        /// Método responsável por buscar uma avaliação a partir do seu Id
        /// </summary>
        /// <param name="avaliacaoId"></param>
        /// <returns></returns>
        [HttpGet("BuscarAvaliacaoPorId/{avaliacaoId}")]
        public async Task<ActionResult<AvaliacaoModel>> BuscarAvaliacaoPorId(int avaliacaoId)
        {
            try
            {
                var avaliacao = await _avaliacaoService.BuscarAvaliacaoPorId(avaliacaoId);
                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("buscar avaliação por Id", ex);
            }
        }

        /// <summary>
        /// Método responsável por escluir avaliação a partir do seu Id
        /// </summary>
        /// <param name="avaliacaoId"></param>
        /// <returns></returns>
        [HttpDelete("ExcluirAvaliacao/{avaliacaoId}")]
        public async Task<IActionResult> ExcluirAvaliacao(int avaliacaoId)
        {
            try
            {
                var excluiu = await _avaliacaoService.ExcluirAvaliacao(avaliacaoId);
                return Ok(excluiu);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("excluir avaliação", ex);
            }
        }

        /// <summary>
        /// Método responsável por buscar todas as avaliações cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet("BuscarTodasAvaliacoes")]
        public async Task<ActionResult<IEnumerable<AvaliacaoModel>>> BuscarTodasAvaliacoes()
        {
            try
            {
                var listaAvaliacoes = await _avaliacaoService.BuscarTodasAvaliacoes();
                return Ok(listaAvaliacoes);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("listar todas avaliações", ex);
            }
        }
    }
}
