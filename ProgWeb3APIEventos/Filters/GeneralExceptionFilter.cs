using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProgWeb3APIEventos.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado.",
                Detail = "Tente novamente!",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine(@$"Tipo da exceção:  {context.Exception.GetType().Name}
Mensagem:  {context.Exception.Message}
Stack trace  {context.Exception.StackTrace}");

            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    problem.Status = 503;
                    problem.Detail = "Erro inesperado ao se comunicar com banco de dados!";
                    context.Result = new ObjectResult(problem);
                    break;
                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    problem.Status = 417;
                    problem.Title = "Erro inesperado no sistema.";
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            };
        }
    }
}
