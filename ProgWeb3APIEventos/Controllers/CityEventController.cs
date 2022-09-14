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
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/eventos/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> Get()
        {
            if (_cityEventService.GetAllEvents().Count == 0)
            {
                return NotFound();
            }
            return Ok(_cityEventService.GetAllEvents());
        }

        [HttpGet("/evento/titulo{title:alpha}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetByTitle(string title)
        {
            if (!_cityEventService.GetByTitle(title).Any())
            {
                return NotFound();
            }

            return Ok(_cityEventService.GetByTitle(title));
        }

        [HttpGet("/evento/local{local:alpha}/data{date:datetime}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetByLocalAndDate(string local, DateTime date)
        {
            if (_cityEventService.GetByLocalAndDate(local, date).Count == 0)
            {
                return NotFound();
            }

            return Ok(_cityEventService.GetByLocalAndDate(local, date));
        }

        [HttpGet("/evento/precoentre{minPrice:decimal}&{maxPrice:decimal}/data{eventDate:datetime}/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> GetByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime eventDate)
        {
            if (_cityEventService.GetByPriceAndDate(minPrice, maxPrice, eventDate).Count == 0)
            {
                return NotFound();
            }

            return Ok(_cityEventService.GetByPriceAndDate(minPrice, maxPrice, eventDate));
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CanBeDeletedActionFilter))]
        public IActionResult DeleteEvent(long id)
        {
            if (!_cityEventService.DeleteEvent(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}