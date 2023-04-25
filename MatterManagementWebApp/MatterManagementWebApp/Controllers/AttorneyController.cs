using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatterManagementWebApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttorneyController : ControllerBase
    {
        private readonly IAttorneyRepository _attorneyRepository;

        public AttorneyController(IAttorneyRepository attorneyRepository)
        {
            _attorneyRepository = attorneyRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var attorneys = _attorneyRepository.GetAll();
            return Ok(attorneys);
        }

        /// <summary>
        /// Deletes a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">successfully received attorney values</response>
        /// <response code="404">attorney with this id not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var attorney = _attorneyRepository.GetById(id);
            if (attorney == null)
            {
                return NotFound();
            }
            return Ok(attorney);
        }

        /// <summary>
        /// Added a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     ADD /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">attorney added successfully</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] AttorneyDto attorney)
        {
            if (attorney == null)
            {
                return BadRequest();
            }

            _attorneyRepository.Add(attorney);
            return CreatedAtAction(nameof(GetById), new { id = attorney.AttorneyId }, attorney);
        }


        /// <summary>
        /// Updates a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     UPDATE /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">attorney updated successfully</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("attorney/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] AttorneyDto attorney)
        {
            if (attorney == null || id != attorney.AttorneyId)
            {
                return BadRequest();
            }

            var existingAttorney = _attorneyRepository.GetById(id);
            if (existingAttorney == null)
            {
                return NotFound();
            }

            _attorneyRepository.Update(attorney);
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a attorney.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/attorney/{id}
        ///
        /// </remarks>
        /// <response code="200">attorney deleted successfully</response>
        /// <response code="404">attorney not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            var attorney = _attorneyRepository.GetById(id);
            if (attorney == null)
            {
                return NotFound();
            }

            _attorneyRepository.Delete(id);
            return new NoContentResult();

        }
    }

}
