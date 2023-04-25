 using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatterManagementWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;

        public ClientController(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("/api/Clients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _clientRepository.Add(client);

            return CreatedAtAction(nameof(GetById), new { ClientId = client.ClientId }, client);
        }

        [HttpGet("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int ClientId)
        {
            var client = _clientRepository.GetById(ClientId);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpGet("Clients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var clients = _clientRepository.GetAll();

            return Ok(clients);
        }

        [HttpPut("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int ClientId, [FromBody] ClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ClientId != client.ClientId)
            {
                return BadRequest();
            }

            _clientRepository.Update(client);

            return NoContent();
        }

        [HttpDelete("Client/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int ClientId)
        {
            var client = _clientRepository.GetById(ClientId);

            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.Delete(ClientId);

            return NoContent();
        }
    }

}
