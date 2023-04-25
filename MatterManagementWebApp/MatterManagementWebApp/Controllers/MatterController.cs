using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatterManagementWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatterController : ControllerBase
    {
        private readonly MatterRepository _matterRepository;

        public MatterController(MatterRepository matterRepository)
        {
            _matterRepository = matterRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] MatterDto matter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _matterRepository.Add(matter);

            return CreatedAtAction(nameof(GetById), new { id = matter.MatterId }, matter);
        }

        [HttpGet("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var matter = _matterRepository.GetById(id);

            if (matter == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(matter);
            }
        }
        [HttpGet("/GetMattersByClient/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetMattersByClient(int ClientId)
        {
            var matter = _matterRepository.GetMattersByClient(ClientId);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("/GetInvoicesByMatter/{MatterId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetInvoicesByMatter(int matterId, InvoiceDto invoice)
        {
            var matter = _matterRepository.GetInvoicesByMatter(matterId,invoice);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("/GetLastWeeksBillingByAttorney/{AttorneyId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetLastWeeksBillingByAttorney(int attorneyId, InvoiceDto invoice)
        {
            var matter = _matterRepository.GetLastWeeksBillingByAttorney(attorneyId,invoice);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("/GetAllMattersByClients/{Matter}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMattersByClients(MatterDto Matter)
        {
            var matter = _matterRepository.GetAllMattersByClients(Matter);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }
        [HttpGet("/GetInvoiceByMatter/{MatterId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllInvoicesByMatters(int matterId, InvoiceDto invoice)
        {
            var matter = _matterRepository.GetInvoicesByMatter(matterId, invoice);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("/GetAllInvoiceByMatters/{Invoice}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllInvoicesByMatters(InvoiceDto invoice)
        {
            var matter = _matterRepository.GetAllInvoicesByMatters(invoice);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("matters")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var matters = _matterRepository.GetAll();

            return Ok(matters);
        }

        [HttpPut("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] MatterDto matter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matter.MatterId)
            {
                return BadRequest();
            }

            _matterRepository.Update(matter);

            return NoContent();
        }

        [HttpDelete("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            var matter = _matterRepository.GetById(id);

            if (matter == null)
            {
                return NotFound();
            }

            _matterRepository.Delete(id);

            return NoContent();
        }
    }

}
