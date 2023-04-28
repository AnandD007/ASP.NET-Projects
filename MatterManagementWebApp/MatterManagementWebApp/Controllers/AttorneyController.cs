using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using MatterManagementWebApp.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatterManagementWebApp.Api;

namespace MatterManagementWebApp.Api.Controllers
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
        public IActionResult GetAll()
        {
            var attorneys = _attorneyRepository.GetAll();
            if (attorneys != null) {
                ResponseStatus response = new
                        ResponseStatus(StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, attorneys);
                return Ok(response);
               }
            return NoContent();
        }

        /// <summary>
        /// Get a attorney By AttorneyId.
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
            if (attorney != null)
            {
                ResponseStatus attorneyExistsResponse = new
                    (StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, attorney);
                return Ok(attorneyExistsResponse);
            }
            ResponseStatus attorneyNotExistsresponse = new
                (StatusCodes.Status404NotFound, ConstantStatusMessages.AttorneyDoesNotExist, null);
            return NotFound(attorneyNotExistsresponse);
        }
        /// <summary>
        /// Get Attorneys By JurisdictionId.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/GetAttorneysByJurisdiction/{id}
        ///
        /// </remarks>
        /// <response code="200">successfully received attorney values</response>
        /// <response code="404">attorney with this id not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("GetAttorneysByJurisdiction/{id}")]
        public IActionResult GetAttorneysByJurisdiction(int id)
        {
            List<AttorneyDto> attorneys = _attorneyRepository.GetAttorniesByJurisdiction(id);
            if (attorneys == null)
            {
                ResponseStatus notFoundResponse = new
                (StatusCodes.Status404NotFound, ConstantStatusMessages.AttorneysByJurisdictionNotFound, attorneys);
                return NotFound(notFoundResponse);
            }
            else
            {
                ResponseStatus responseExists = new
                (StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, attorneys);
                return Ok(responseExists);
            }
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
            ResponseStatus response = new
            (StatusCodes.Status200OK, ConstantStatusMessages.DataAddedSuccessfully, attorney);
            return Ok(response);
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
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] AttorneyDto attorney)
        {
            int result = _attorneyRepository.Update(attorney);
            if (result.Equals(0))
            {
                ResponseStatus response = new
                    (StatusCodes.Status404NotFound, ConstantStatusMessages.AttorneyDoesNotExist,
                                                    ConstantStatusMessages.AttorneyDoesNotExist);
                return NotFound(response);
            }
            else
            {
                ResponseStatus response = new(StatusCodes.Status200OK, ConstantStatusMessages.DataUpdatedSuccessfully, result);
                return Ok(response);
            }

     
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
            int result = _attorneyRepository.Delete(id);
            if (result.Equals(0))
            {
                ResponseStatus response =
                    new(StatusCodes.Status400BadRequest, ConstantStatusMessages.AttorneyDoesNotExist, result);
                return BadRequest(response);
            }
            else
            {
                ResponseStatus response = new(StatusCodes.Status200OK, ConstantStatusMessages.DataDeletedSuccessfully, null);
                return Ok(response);
            }


        }
    }

}
