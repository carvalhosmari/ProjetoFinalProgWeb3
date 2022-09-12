using Microsoft.AspNetCore.Mvc;
using ProgWeb3APIEventos.Core.Interface;
using ProgWeb3APIEventos.Core.Model;

namespace ProgWeb3APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/eventos/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> Get()
        {
            return Ok(_cityEventService.GetAllEvents());
        }

        [HttpGet("/evento/{title}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetByTitle(string title)
        {
            if (_cityEventService.GetByTitle(title) == null)
            {
                return NotFound();
            }

            return Ok(_cityEventService.GetByTitle(title));
        }

        [HttpGet("/evento/{local}&{date}/consultar")]
        public ActionResult<List<CityEvent>> GetByLocalAndDate(string local, DateTime date)
        {
            return Ok(_cityEventService.GetByLocalAndDate(local, date));
        }

        [HttpPost("/evento/inserir")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CityEvent> InsertEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.InsertEvent(cityEvent))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InsertEvent), cityEvent);
        }

        [HttpPut("/evento/{id}/atualizar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateEvent(long id, CityEvent cityEvent)
        {
            if (!_cityEventService.UpdateEvent(id, cityEvent))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("/evento/{id}/deletar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteEvent(long id)
        {
            if (_cityEventService.DeleteEvent(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}