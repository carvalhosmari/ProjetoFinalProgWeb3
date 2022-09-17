using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProgWeb3APIEventos.Core.Interface;

namespace ProgWeb3APIEventos.Filters
{
    public class CanBeDeletedActionFilter : ActionFilterAttribute
    {
        readonly ICityEventService _cityEventService;

        public CanBeDeletedActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idEvent = (long)context.ActionArguments["id"];

            if (_cityEventService.HaveReservation(idEvent) || !_cityEventService.IsActive(idEvent))
            {
                var problem = new ProblemDetails
                {
                    Status = 400,
                    Title = "Bad request",
                    Detail = "Não foi possivel deletar o evento desejado, pois o mesmo encontra-se ativo e/ou possui reservas.",
                };

                context.Result = new ObjectResult(problem);
            }
        }
    }
}
