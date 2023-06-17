 using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using MatterManagementWebApp.Api;
using MatterManagementWebApp.Services.Models;

namespace MatterManagementWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
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
        [HttpPost("/api/Clients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(ClientDto client)
        {
            int result = _clientRepository.Add(client);
            ResponseStatus response = new
            (StatusCodes.Status200OK, ConstantStatusMessages.DataAddedSuccessfully, result);
            return Ok(response);
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
        [HttpGet("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int ClientId)
        {
            var client = _clientRepository.GetById(ClientId);

            if (client != null)
            {
                ResponseStatus response = new
                    ResponseStatus(StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, client);
                return Ok(response);
            }
            return NoContent();
        }
        /// <summary>
        /// Get all attorneys.
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
        [HttpGet("Clients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var clients = _clientRepository.GetAll();

            if (clients != null)
            {
                ResponseStatus clientExistsResponse = new
                    (StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, clients);
                return Ok(clientExistsResponse);
            }
            ResponseStatus clientNotExistsResponse = new
                (StatusCodes.Status404NotFound, ConstantStatusMessages.ClientDoesNotExist, null);
            return NotFound(clientNotExistsResponse);
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
        [HttpPut("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int ClientId, [FromBody] ClientDto client)
        {
            int result = _clientRepository.Update(client);
            if (result.Equals(0))
            {
                ResponseStatus response = new
                    (StatusCodes.Status404NotFound, ConstantStatusMessages.ClientDoesNotExist,
                                                    ConstantStatusMessages.ClientDoesNotExist);
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
        [HttpDelete("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int ClientId)
        {
            var client = _clientRepository.GetById(ClientId);
            int result = _clientRepository.Delete(ClientId);
            if (result.Equals(0))
            {
                ResponseStatus response =
                    new(StatusCodes.Status400BadRequest, ConstantStatusMessages.ClientDoesNotExist, result);
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
