using CustomersWebAPIv1;
using Microsoft.AspNetCore.Mvc;



namespace CustomersWebAPIv1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public static CustomerDetails allCustomersData = new CustomerDetails();

        // GET: api/CustomerInfo

        /// <summary>
        /// Returns All CustomerInfo Data
        /// </summary>
        /// <response code="200">  If Customers are Found and Response is Given</response>
        /// <response code="400">  If Anything is Missing from Client Side's Request</response>
        /// <response code="404">  If Controller or Data not Found</response>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var response = new
            {
                statusCode = 200,
                message = "Data Retrieval Successful",
                data = allCustomersData
            };
            return new ObjectResult(response);
        }

        // GET api/CustomerInfo/{CustomerID}

        /// <summary>
        /// Returns a single CustomerInfo Data by taking the CustomerInfo's ID
        /// </summary>
        /// <response code="200">  If CustomerInfo is Found and Response is Given</response>
        /// <response code="400">  If Controller parameter is Missing</response>
        /// <response code="404">  If Controller or Data not Found</response>
        /// 
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            foreach (var customer in allCustomersData.customers)
            {
                if (customer.customerId == id)
                {
                    var response1 = new
                    {
                        statusCode = 200,
                        message = "Data Retrieval Successful",
                        data = allCustomersData
                    };
                    return new ObjectResult(response1);
                }
            }
            CustomerInfo nullCustomer = new CustomerInfo();
            nullCustomer.customerId = null;
            nullCustomer.locations = null;
            var response2 = new
            {
                statusCode = 404,
                message = "Unsuccessful Data Retrieval. CustomerInfo with this ID does not Exist",
                data = nullCustomer
            };
            return NotFound(response2);
        }

        

        // POST api/CustomerInfo

        /// <summary>
        /// Takes a Single CustomerInfo Data and Sends through API
        /// </summary>
        /// <response code="200">  If CustomerInfo Data is Submitted Successfully</response>
        /// <response code="202">  If Request is Accepted, but CustomerInfo with the same ID already Exists</response>
        /// <response code="400">  If Anything is Missing from Client Side's Request</response>
        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerInfo value)
        {
            foreach (var customer in allCustomersData.customers.Where(w => w.customerId == value.customerId))
            {
                var response = new
                {
                    statusCode = 202,
                    message = "Customer Details with the same ID already Exists",
                    data = customer
                };
                return Accepted(response);
            }
            allCustomersData.customers.Add(value);
            var response2 = new
            {
                statusCode = 200,
                message = "Customer Details Added Successfully",
                data = value
            };
            return Created($"~api/CustomerInfo/{value.customerId}", response2);
        }

        // PUT api/CustomerInfo/{id}

        /// <summary>
        /// Updates a Customer Data by taking as Input the Customer ID of Existing Customer
        /// </summary>
        /// <response code="200">  If CustomerInfo is Found then the data will be Modified</response>
        /// <response code="400">  If Controller parameter is Missing</response>
        /// <response code="404">  If Controller or Data not Found</response>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, [FromBody] CustomerInfo inputCustomer)
        {
            foreach (var customer in allCustomersData.customers.Where(w => w.customerId == id))
            {
                customer.customerId = inputCustomer.customerId;
                customer.locations = inputCustomer.locations;
                var response = new
                {
                    statusCode = 200,
                    message = "Data Updated Successfully",
                    data = customer
                };
                return Ok(response);
            }                                               //What is updated ID already exists??
            var response2 = new
            {
                statusCode = 404,
                message = "Unsuccessful Data Updation. CustomerInfo with this ID does not Exist",
                data = new CustomerInfo()
        };
            return NotFound(response2);
        }

        // DELETE api/CustomerInfo/{id}

        /// <summary>
        /// Deletes all the Customer Data if the Locations are null by taking as Input of customer ID of existing Customer
        /// </summary>
        /// <response code="204">  If CustomerInfo is Deleted Successfully</response>
        /// <response code="202">  If Request is Accepted, but CustomerInfo has Existing Locations</response>
        /// <response code="400">  If Controller parameter is Missing</response>
        /// <response code="404">  If Controller or Data not Found</response>
        /// 
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            foreach (var customer in allCustomersData.customers.Where(w => w.customerId == id))
            {
                if (customer.locations[0] == null)
                {
                    allCustomersData.customers.Remove(customer);
                    var userResponse1 = new
                    {
                        statusCode = 204,
                        message = "Data is Deletion Successful",
                        data = customer
                    };
                    return Ok(userResponse1);
                }
                else
                {
                    var userResponse2 = new
                    {
                        statusCode = 202,
                        message = "Unsuccessful Data Deletion - CustomerInfo Record Contains Locations. Remove Locations First",
                        data = customer
                    };
                    return Accepted(userResponse2);
                }
            }
            CustomerInfo nullCustomer = new CustomerInfo();
            nullCustomer.customerId = null;
            nullCustomer.locations = null;
            var userResponse3 = new
            {

                statusCode = 404,
                message = "Unsuccessful Data Deletion. CustomerInfo with this ID does not Exist",
                data = nullCustomer
            };
            return NotFound(userResponse3);
        }
    }
}
