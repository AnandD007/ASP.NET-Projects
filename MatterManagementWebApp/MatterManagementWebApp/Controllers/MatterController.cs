
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
        private readonly IMatterRepository _matterRepository;

        public MatterController(IMatterRepository matterRepository)
        {
            _matterRepository = matterRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(MatterDto matter)
        {
            int result = _matterRepository.Add(matter);
            if (result.Equals(0))
            {
                ResponseStatus response = new
                    (StatusCodes.Status400BadRequest, ConstantStatusMessages.NoMatchingJurisdiction,
                                                      ConstantStatusMessages.NoMatchingJurisdiction);
                return BadRequest(response);
            }
            else if (result.Equals(-1))
            {
                ResponseStatus response = new
                    (StatusCodes.Status400BadRequest, ConstantStatusMessages.NoMatchingJurisdiction,
                                                      ConstantStatusMessages.NoMatchingJurisdiction);
                return BadRequest(response);
            }
            else
            {
                ResponseStatus response = new
                (StatusCodes.Status200OK, ConstantStatusMessages.DataAddedSuccessfully, result);
                return Ok(response);
            }
        }

        [HttpGet("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var matter = _matterRepository.GetById(id);

            if (matter != null)
            {
                ResponseStatus matterExistsResponse = new
                (StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, matter);
                return Ok(matterExistsResponse);
            }
            ResponseStatus matterNotExistsResponse = new
            (StatusCodes.Status404NotFound, ConstantStatusMessages.MatterDoesNotExist, null);
            return NotFound(matterNotExistsResponse);
        }
        [HttpGet("GetMattersForClient/{ClientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetMattersByClient(int ClientId)
        {
            var matter = _matterRepository.GetMattersForClient(ClientId);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("GetLastWeeksBillingForAttorney/{AttorneyId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetLastWeeksBillingForAttorney(int attorneyId)
        {
            var matter = _matterRepository.GetLastWeeksBillingForAttorney(attorneyId);

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("GetLastWeeksBillingByAttorneys")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetLastWeeksBillingByAttorneys()
        {
            var matter = _matterRepository.GetLastWeeksBillingByAttorneys();

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("GetAllMattersByClients")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMattersByClients()
        {
            var matter = _matterRepository.GetAllMattersByClients();

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("GetAllInvoicesByMatters")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllInvoicesByMatters()
        {
            var matter = _matterRepository.GetAllInvoicesByMatters();

            if (matter == null)
            {
                return NotFound();
            }

            return Ok(matter);
        }

        [HttpGet("GetAllInvoicesForMatter")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetInvoicesForMatter(int id)
        {
            List<InvoiceMatterDto> result = _matterRepository.GetInvoicesForMatter(id);
            if (result == null)
            {
                ResponseStatus noInvoicesByMatterResponse = new
                (StatusCodes.Status404NotFound, ConstantStatusMessages.InvoicesByMatterNotFound, null);
                return NotFound(noInvoicesByMatterResponse);
            }
            else
            {
                ResponseStatus invoicesByMatterExistsResponse = new
                (StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, result);
                return Ok(invoicesByMatterExistsResponse);
            }
        }

        [HttpGet("matters")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var matters = _matterRepository.GetAll();
            if (matters != null)
            {
                ResponseStatus response = new
                    ResponseStatus(StatusCodes.Status200OK, ConstantStatusMessages.DataRetrievedSuccessfully, matters);
                return Ok(response);
            }
            return NoContent();

        }

        [HttpPut("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] MatterDto matter)
        {
            int result = _matterRepository.Update(matter);
            if (result.Equals(0))
            {
                ResponseStatus response = new
                    (StatusCodes.Status404NotFound, ConstantStatusMessages.MatterDoesNotExist,
                                                    ConstantStatusMessages.MatterDoesNotExist);
                return NotFound(response);
            }
            else
            {
                ResponseStatus response = new(StatusCodes.Status200OK, ConstantStatusMessages.DataUpdatedSuccessfully, result);
                return Ok(response);
            }
        }

        [HttpDelete("matter/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            int result = _matterRepository.Delete(id);

            if (result.Equals(0))
            {
                ResponseStatus response =
                    new(StatusCodes.Status400BadRequest, ConstantStatusMessages.MatterDoesNotExist, result);
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
