using Microsoft.AspNetCore.Mvc;



namespace CustomersWebAPIv1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public static CustomerDetails allCustomersData = new CustomerDetails();

        // GET: api/Customers

        /// <summary>
        /// Returns All Customers Data
        /// </summary>
        /// <response code="200">  If Customers are Found and Response is Given</response>
        /// <response code="400">  If Anything is Missing from Client Side's Request</response>
        /// <response code="404">  If Controller or Data not Found</response>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var userOkResponse = new
            {
                statusCode = 200,
                message = "Data Retrieval Successful",
                data = allCustomersData
            };
            return new ObjectResult(userOkResponse);
        }

        // GET api/Customers/{CustomerID}

        /// <summary>
        /// Returns a single Customers Data by taking the Customers's ID
        /// </summary>
        /// <response code="200">  If Customers is Found and Response is Given</response>
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
                    var userOkResponse = new
                    {
                        statusCode = 200,
                        message = "Data Retrieval Successful",
                        data = customer
                    };
                    return new ObjectResult(userOkResponse);
                }
            }
            Customers nullCustomer = new Customers();
            nullCustomer.customerId = null;
            nullCustomer.locations = null;
            var userNotFoundResponse = new
            {
                statusCode = 404,
                message = "Unsuccessful Data Retrieval. Customers with this ID does not Exist",
                data = nullCustomer
            };
            return NotFound(userNotFoundResponse);
        }



        // POST api/Customers

        /// <summary>
        /// Takes a Single Customers Data and Sends through API
        /// </summary>
        /// <response code="200">  If Customers Data is Submitted Successfully</response>
        /// <response code="202">  If Request is Accepted, but Customers with the same ID already Exists</response>
        /// <response code="400">  If Anything is Missing from Client Side's Request</response>
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customers value)
        {
            var customer = allCustomersData.customers.FirstOrDefault(w => w.customerId == value.customerId);
            if (customer != null)
            {
                var userIdExistresponse = new
                {
                    statusCode = 202,
                    message = "Customer Details with the same ID already Exists",
                    data = customer
                };
                return Accepted(userIdExistresponse);
            }
            allCustomersData.customers.Add(value);
            var userOkResponse = new
            {
                statusCode = 200,
                message = "Customer Details Added Successfully",
                data = value
            };
            return Created($"~api/Customers/{value.customerId}", userOkResponse);
        }

        // PUT api/Customers/{id}

        /// <summary>
        /// Updates a Customer Data by taking as Input the Customer ID of Existing Customer
        /// </summary>
        /// <response code="200">  If Customers is Found then the data will be Modified</response>
        /// <response code="400">  If Controller parameter is Missing</response>
        /// <response code="404">  If Controller or Data not Found</response>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, [FromBody] Customers inputCustomer)
        {
            var customer = allCustomersData.customers.FirstOrDefault(w => w.customerId == id);
            customer.customerId = inputCustomer.customerId;
            customer.locations = inputCustomer.locations;
            var userOkResponse = new
            {
                statusCode = 200,
                message = "Data Updated Successfully",
                data = customer
            };
            return Ok(userOkResponse);
            var userNotFoundResponse = new
            {
                statusCode = 404,
                message = "Unsuccessful Data Updation. Customers with this ID does not Exist",
                data = new Customers()
            };
            return NotFound(userNotFoundResponse);
        }

        // DELETE api/Customers/{id}

        /// <summary>
        /// Deletes all the Customer Data if the Locations are null by taking as Input of customer ID of existing Customer
        /// </summary>
        /// <response code="204">  If Customers is Deleted Successfully</response>
        /// <response code="202">  If Request is Accepted, but Customers has Existing Locations</response>
        /// <response code="400">  If Controller parameter is Missing</response>
        /// <response code="404">  If Controller or Data not Found</response>
        /// 
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            var customer = allCustomersData.customers.FirstOrDefault(w => w.customerId == id);
            if (customer.locations[0] != null)
            {
                allCustomersData.customers.Remove(customer);
                var userDeletionResponse = new
                {
                    statusCode = 204,
                    message = "Data is Deletion Successful",
                    data = customer
                };
                return Ok(userDeletionResponse);
            }
            else
            {
                var userUnsuccessfulResponse = new
                {
                    statusCode = 202,
                    message = "Unsuccessful Data Deletion - Customers Record Contains Locations. Remove Locations First",
                    data = customer
                };
                return Accepted(userUnsuccessfulResponse);
            }
            Customers nullCustomer = new Customers();
            nullCustomer.customerId = null;
            nullCustomer.locations = null;
            var userNotFoundResponse = new
            {

                statusCode = 404,
                message = "Unsuccessful Data Deletion. Customers with this ID does not Exist",
                data = nullCustomer
            };
            return NotFound(userNotFoundResponse);
        }
    }
}
