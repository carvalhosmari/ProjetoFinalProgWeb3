using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProgWeb3APIEventos.Core.Interface;

namespace ProgWeb3APIEventos.Filters
{
    public class CanBeDeletedActionFilter : ActionFilterAttribute
    {
        private readonly ICityEventService _cityEventService;

        public CanBeDeletedActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idEvent = (long)context.ActionArguments["id"];

            if (_cityEventService.HaveReservation(idEvent) || !_cityEventService.IsActive(idEvent) || _cityEventService.IsActive(idEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
