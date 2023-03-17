using CustomerWebAPI.Services.Models;
using CustomerWebAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomersRepository _customers;
        private readonly ICustomerLocationsRepository _customerLocations;

        public CustomerController(ICustomersRepository icustomers, ICustomerLocationsRepository icustomerLocations)
        {
            _customers = icustomers;
            _customerLocations = icustomerLocations;
        }

        /// <summary>
        ///  Returns All Customers Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            if (_customers.GetAllCustomers != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Data Retrieval Successful",
                    data = _customers.GetAllCustomers()
                };
                return Ok(okResponse);
            }
            var customerIdExistResponse = new
            {
                statusCode = HttpStatusCode.BadRequest,
                message = "Customer Details Not Found"
            };
            return BadRequest(customerIdExistResponse);
        }
        /// <summary>
        /// Returns a single Customers Data by taking the Customers's ID
        /// </summary>
        /// <param name="customerId">Enter the Customer Id</param>
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int customerId)
        {

            Customers customer = _customers.GetCustomerById(customerId);
            if (customer != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Data Retrieval Successful",
                    data = customer
                };
                return Ok(okResponse);
            }
            var notFoundResponse = new
            {
                statusCode = HttpStatusCode.NotFound,
                message = "Unsuccessful Data Retrieval. Customers with this ID does not Exist",
            };
            return NotFound(notFoundResponse);
        }



        // POST api/Customers

        /// <summary>
        /// Takes a Single Customers Data and Sends through API
        /// </summary>
        /// <param name="value">Enter the Customer Details</param>
        [HttpPost]
        public IActionResult AddCustomer(Customers value)
        {
            Customers customer = _customers.AddCustomer(value);
            if (customer != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Customer Details Added Successfully",
                    data = customer
                };
                return Created($"~api/Customers/{value.CustomerId}", okResponse);
            }
            var customerIdExistResponse = new
            {
                statusCode = HttpStatusCode.BadRequest,
                message = "Incorrect Customer Details",
                data = customer
            };
            return BadRequest(customerIdExistResponse);
        }

        // PUT api/Customers/{id}

        /// <summary>
        /// Updates a Customer Data by taking as Input the Customer ID of Existing Customer
        /// </summary>
        /// <param name="customerId">Enter the Customer Id</param>
        /// <param name="inputCustomer">Enter the Customer Details</param>
        [HttpPut("{customerId}")]
       public  IActionResult UpdateCustomer(int customerId, [FromBody] Customers inputCustomer)
        {
            Customers customer = _customers.UpdateCustomer(customerId, inputCustomer);
            if (customerId != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Data Updated Successfully",
                    data = customer
                };
                return Ok(okResponse);
            }
            var notFoundResponse = new
            {
                statusCode = HttpStatusCode.NotFound,
                message = "Unsuccessful Data Updation. Customers with this ID does not Exist",
                data = customer
            };
            return NotFound(notFoundResponse);
        }

        // DELETE api/Customers/{id}

        /// <summary>
        /// Deletes all the Customer Data if the CustomerLocation are null by taking as Input of customer ID of existing Customer
        /// </summary>
        /// <param name="customerId">Enter the Customer Id</param>
        /// 
        [HttpDelete("{customerId}")]
       public IActionResult DeleteCustomer(int customerId)
        {
            Customers customer = _customers.DeleteCustomer(customerId);
            if (customer.CustomerId != customerId)
            {
                var notFoundResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Unsuccessful Data Deletion. Customers with this ID does not Exist",
                    data = customer.CustomerLocation
                };
                return NotFound(notFoundResponse);
            }
            else
            {
                if (customer.CustomerLocation != null && customer.CustomerLocation.Any())
                {
                    var unsuccessfulResponse = new
                    {
                        statusCode = HttpStatusCode.Accepted,
                        message = "Unsuccessful Data Deletion - Customers Record Contains CustomerLocation. Remove CustomerLocation First",
                        data = customer
                    };
                    return Accepted(unsuccessfulResponse);
                }
                var deletionResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Data is Deletion Successful",
                    data = customer
                };
                return Ok(deletionResponse);
            }
        }


        // Customer CustomerLocation

        /// <summary>
        /// Add the Location Details of the Customer
        /// </summary>
        /// <param name="customerId">Enter Customer Id.</param>      
        /// <param name="newLocation">Enter Location details</param>        
        [Route("Location")]
        [HttpPost]
        
        public IActionResult AddCustomerLocation(int customerId, CustomerLocation location)
        {
            CustomerLocation locations = _customerLocations.AddCustomerLocation(customerId, location);
            if (locations != null)
            {
                var addLocationResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Customer Location Details Added Successfully",
                    data = locations
                };
                return Ok(addLocationResponse);
            }
            var locationNotFoundResponse = new
            {
                statusCode = HttpStatusCode.NotFound,
                message = "Customer Location Details Added Successfully",
                data = locations
            };
            return NotFound(locationNotFoundResponse);
        }

        /// <summary>
        /// Update the Location Details of the Customer
        /// </summary>
        /// <param name="customerId">Enter the Customer Id</param>      
        /// <param name="locationId">Enter Location Id to Update Location</param>   
        /// <param name="location">Enter Location details</param>  
        [Route("Location")]
        [HttpPut]
        public IActionResult UpdateCustomerLocation(int customerId, int locationId, CustomerLocation location)
        {

            CustomerLocation locations = _customerLocations.UpdateCustomerLocation(customerId, locationId, location);
            if (locations != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Location Data Updated Successfully",
                    data = locations
                };
                return Ok(okResponse);
            }
            var locationNotFoundResponse = new
            {
                statusCode = HttpStatusCode.NotFound,
                message = "Unsuccessful Data Updation. Customers with this ID does not Exist",
                data = locations
            };
            return NotFound(locationNotFoundResponse);
        }

        /// <summary>
        /// Get the Location of Customer
        /// </summary>
        /// <param name="customerId">Enter Customer Id.</param>      
        /// <param name="locationId">Enter Location Id to Get Location.</param>
        [Route("Location")]
        [HttpGet]
        public IActionResult GetLocationByCustomerId(int customerId, int locationId)
        {
            CustomerLocation locations = _customerLocations.GetLocationByCustomerId(customerId, locationId);
            if (locations != null)
            {
                var okResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Location Data Retrieval Successful",
                    data = locations
                };
                return Ok(okResponse);
            }
            var notFoundResponse = new
            {
                statusCode = HttpStatusCode.NotFound,
                message = "Unsuccessful Location Data Retrieval. Customer Location with this ID does not Exist",
                data = locations
            };
            return NotFound(notFoundResponse);
        }

        /// <summary>
        /// Delete the Location of Customer
        /// </summary>
        /// <param name="customerId">Enter the Customer Id</param>
        /// <param name="locationId">Enter Location Id to delete Location.</param>
        [Route("Location")]
        [HttpDelete]
        public IActionResult DeleteCustomerLocation(int customerId, int locationId)
        {
            CustomerLocation locations = _customerLocations.DeleteCustomerLocation(customerId, locationId);
            if (locations != null)
            {
                var locationDeletionResponse = new
                {
                    statusCode = HttpStatusCode.OK,
                    message = "Location Data is Deletion Successful",
                    data = locations
                };
                return Ok(locationDeletionResponse);
            }
            if(locations == null)
            {

            }
            var locationNotFoundResponse = new
            {

                statusCode = HttpStatusCode.BadRequest,
                message = "Unsuccessful Location Data Deletion. Location with this ID does not Exist",
                data = locations
            };
            return NotFound(locationNotFoundResponse);

        }
    }
}
