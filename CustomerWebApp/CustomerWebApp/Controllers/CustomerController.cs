using Microsoft.AspNetCore.Mvc;
using CustomerWebApp.Services.Data.Entities;
using CustomerWebApp.Services.Repository;
using CustomerWebApp.Api.Models;

namespace CustomerWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _CustomerRepository;
        public CustomerController(ICustomer customerRepository)
        {
            _CustomerRepository = customerRepository;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Customer> result = _CustomerRepository.GetAllCustomer();
            if (result != null)
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.dataRetrievedSuccessfully, result);
                return Ok(response);
            }
            return NoContent();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{CustomerId}")]
        public IActionResult Get(int CustomerId)
        {
            Customer result = _CustomerRepository.GetCustomer(CustomerId);
            if (result != null)
            {
                Response customerExistsResponse = new
                    (StatusCodes.Status200OK, ConstantMessages.dataRetrievedSuccessfully, result);
                return Ok(customerExistsResponse);
            }
            Response customerNotExistsresponse = new
                (StatusCodes.Status404NotFound, ConstantMessages.customerDoesNotExist, null);
            return NotFound(customerNotExistsresponse);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            int result = _CustomerRepository.AddCustomer(customer);
            if (result.Equals(-1))
            {
                Response response = new
                    (StatusCodes.Status208AlreadyReported, ConstantMessages.customerAlreadyExists, ConstantMessages.customerAlreadyExists);
                return Ok(response);
            }
            else
            {
                Response response = new
                (StatusCodes.Status200OK, ConstantMessages.customerAddedSuccessfully, result);
                return Ok(response);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{CustomerId}")]
        public IActionResult Put(int CustomerId, [FromBody] Customer updatedCustomer)
        {
            int result = _CustomerRepository.UpdateCustomer(CustomerId, updatedCustomer);
            if (result.Equals(-1))
            {
                Response response = new
                    (StatusCodes.Status404NotFound, ConstantMessages.customerDoesNotExist, ConstantMessages.customerDoesNotExist);
                return NotFound(response);
            }
            else if (result.Equals(0))
            {
                Response response = new
                    (StatusCodes.Status400BadRequest, ConstantMessages.locationIdDoesntExist, ConstantMessages.locationIdDoesntExist);
                return BadRequest(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.dataUpdatedSuccessfully, result);
                return Ok(response);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{CustomerId}")]
        public IActionResult Delete(int CustomerId)
        {
            int result = _CustomerRepository.DeleteCustomer(CustomerId);
            if (result.Equals(0))
            {
                Response response = new(StatusCodes.Status400BadRequest, ConstantMessages.dataContainsLocations, result);
                return BadRequest(response);
            }
            else if (result.Equals(-1))
            {
                Response response = new(StatusCodes.Status404NotFound, ConstantMessages.customerDoesNotExist, null);
                return NotFound(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.dataDeletedSuccessfully, null);
                return Ok(response);
            }
        }
    }
}
