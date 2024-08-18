using Microsoft.AspNetCore.Mvc;
using AvaliaMeuCurso.Application.Models.Error;

namespace AvaliaMeuCurso.Application.Helpers
{
    public class ValidadorErro : IValidadorErro
    {
        public ObjectResult TratarErro(string acao, Exception ex)
        {
            return new ObjectResult(new ErroModel
            {
                Mensagem = $"Ocorreu um erro ao tentar {acao}: {ex.Message}",
                Detalhes = ex.StackTrace
            })
            {
                StatusCode = 500
            };
        }
    }

    public interface IValidadorErro
    {
        ObjectResult TratarErro(string acao, Exception ex);
    }
}
