using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatterManagementWebApp.Api.Controllers
{
    // Jurisdiction Controller
    [Route("api/[controller]")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        private readonly IJurisdictionRepository _jurisdictionRepository;

        public JurisdictionController(IJurisdictionRepository jurisdictionRepository)
        {
            _jurisdictionRepository = jurisdictionRepository;
        }

        [HttpGet("/api/Jurisdictions")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var jurisdictions = _jurisdictionRepository.GetAll();

            if (jurisdictions == null)
            {
                return NotFound();
            }

            return Ok(jurisdictions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var jurisdiction = _jurisdictionRepository.GetById(id);

            if (jurisdiction == null)
            {
                return NotFound();
            }

            return Ok(jurisdiction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] JurisdictionDto jurisdiction)
        {
            if (jurisdiction == null)
            {
                return BadRequest();
            }
            _jurisdictionRepository.Add(jurisdiction);

            return CreatedAtAction(nameof(GetById), new { id = jurisdiction.JurisdictionId }, jurisdiction);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] JurisdictionDto jurisdiction)
        {
            if (jurisdiction == null || jurisdiction.JurisdictionId != id)
            {
                return BadRequest();
            }

            var existingJurisdiction = _jurisdictionRepository.GetById(id);

            if (existingJurisdiction == null)
            {
                return NotFound();
            }

            _jurisdictionRepository.Update(jurisdiction);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            var jurisdiction = _jurisdictionRepository.GetById(id);

            if (jurisdiction == null)
            {
                return NotFound();
            }
            _jurisdictionRepository.Delete(id);

            return new NoContentResult();
        }
    }
}
