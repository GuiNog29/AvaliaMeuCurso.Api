using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Helpers;
using AvaliaMeuCurso.Application.Models.Estudante;
using AvaliaMeuCurso.Application.Interfaces.Service;

namespace AvaliaMeuCurso.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudanteController : ControllerBase
    {
        private readonly IEstudanteService _estudanteService;
        private readonly IValidadorErro _validadorErro;

        public EstudanteController(IEstudanteService estudanteService, IValidadorErro validadorErro)
        {
            _estudanteService = estudanteService;
            _validadorErro = validadorErro;
        }

        /// <summary>
        /// Método responsável por cadastrar um novo estudante
        /// </summary>
        /// <param name="estudanteModel"></param>
        /// <returns></returns>
        [HttpPost("CriarNovoEstudante")]
        public async Task<ActionResult<EstudanteModel>> CriarNovoEstudante([FromBody] EstudanteModel estudanteModel)
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
                return _validadorErro.TratarErro("cadastrar estudante", ex);
            }
        }

        /// <summary>
        /// Método responsável por atualizar um estudante
        /// </summary>
        /// <param name="estudanteModel"></param>
        /// <param name="estudanteId"></param>
        /// <returns></returns>
        [HttpPut("AtualizarEstudante/{estudanteId}")]
        public async Task<IActionResult> AtualizarEstudante([FromBody] EstudanteAtualizacaoModel estudanteModel, int estudanteId)
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
                return _validadorErro.TratarErro("atualizar estudante", ex);
            }
        }

        /// <summary>
        /// Método responsável por buscar um estudante a partir do seu Id
        /// </summary>
        /// <param name="estudanteId"></param>
        /// <returns></returns>
        [HttpGet("BuscarEstudantePorId/{estudanteId}")]
        public async Task<ActionResult<EstudanteModel>> BuscarEstudantePorId(int estudanteId)
        {
            try
            {
                var estudante = await _estudanteService.BuscarEstudantePorId(estudanteId);
                return Ok(estudante);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("buscar estudante por Id", ex);
            }
        }

        /// <summary>
        /// Método responsável por excluir um estudante a partir do seu Id
        /// </summary>
        /// <param name="estudanteId"></param>
        /// <returns></returns>
        [HttpDelete("ExcluirEstudante/{estudanteId}")]
        public async Task<IActionResult> ExcluirEstudante(int estudanteId)
        {
            try
            {
                var excluiu = await _estudanteService.ExcluirEstudante(estudanteId);
                if (!excluiu)
                    return NotFound($"Estudante com Id:{estudanteId} não encontrado.");

                return Ok(excluiu);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("excluir estudante", ex);
            }
        }

        /// <summary>
        /// Método responsável por buscar todos os estudantes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet("BuscarTodosEstudantes")]
        public async Task<ActionResult<IEnumerable<EstudanteModel>>> BuscarTodosEstudantes()
        {
            try
            {
                var listaEstudantes = await _estudanteService.BuscarTodosEstudantes();
                return Ok(listaEstudantes);
            }
            catch (Exception ex)
            {
                return _validadorErro.TratarErro("listar todos os estudantes", ex);
            }
        }
    }
}
