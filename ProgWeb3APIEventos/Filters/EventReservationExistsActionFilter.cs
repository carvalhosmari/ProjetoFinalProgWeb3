using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Filters
{
    public class EventReservationExistsActionFilter : ActionFilterAttribute
    {
        readonly IEventReservationService _eventReservationService;

        public EventReservationExistsActionFilter(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var eventReservation = (EventReservation)context.ActionArguments["eventReservation"];

            List<EventReservation> reservations = _eventReservationService.GetAllReservations();

            if (reservations.FindAll(option => option.IdEvent == eventReservation.IdEvent && option.PersonName == eventReservation.PersonName).Any())
            {
                var problem = new ProblemDetails
                {
                    Status = 409,
                    Title = "Conflict",
                    Detail = "Já existe uma reserva para o titular no evento desejado. Favor editar a quantidade da reserva feita anteriormente.",
                };

                context.Result = new ObjectResult(problem);
            }
        }
    }
}
