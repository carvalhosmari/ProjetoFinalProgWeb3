using Microsoft.AspNetCore.Mvc;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/reservas/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> Get()
        {
            return Ok(_eventReservationService.GetAllReservations());
        }

        [HttpPost("/reserva/inserir")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<EventReservation> InsertReservation(EventReservation eventReservation)
        {
            if (_eventReservationService.InsertReservation(eventReservation))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InsertReservation), eventReservation);
        }

        [HttpPut("/reserva/{id}/atualizar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateReservation(long id, EventReservation eventReservation)
        {
            if (_eventReservationService.UpdateReservation(id, eventReservation))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("/reserva/{id}/deletar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteReservation(long id)
        {
            if (_eventReservationService.DeleteReservation(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}