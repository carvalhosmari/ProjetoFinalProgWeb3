using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;
using ProgWeb3APIEventos.Filters;

namespace ProgWeb3APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/reservas/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventReservation>> Get()
        {
            if (!_eventReservationService.GetAllReservations().Any())
            {
                return NotFound();
            }

            return Ok(_eventReservationService.GetAllReservations());
        }

        [HttpGet("/reserva/{personName}/{title}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public ActionResult<List<EventReservation>> GetByPersonNameAndTitle(string personName, string title)
        {
            if (!_eventReservationService.GetByPersonNameAndTitle(personName, title).Any())
            {
                return NotFound();
            }

            return Ok(_eventReservationService.GetByPersonNameAndTitle(personName, title));
        }

        [HttpPost("/reserva/inserir")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(EventReservationExistsActionFilter))]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<EventReservation> InsertReservation(EventReservation eventReservation)
        {
            if (!_eventReservationService.InsertReservation(eventReservation))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InsertReservation), eventReservation);
        }

        [HttpPut("/reserva/{id}/atualizar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReservation(long id, long quantity)
        {
            if (!_eventReservationService.UpdateReservation(id, quantity))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("/reserva/{id}/deletar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteReservation(long id)
        {
            if (!_eventReservationService.DeleteReservation(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}