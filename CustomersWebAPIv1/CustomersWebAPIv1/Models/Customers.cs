

using System.Text.Json.Serialization;


namespace CustomersWebAPIv1
{
    public class Customers
    {
        /// <summary>
        /// Customer Id Details
        /// </summary>
        public string customerId { get; set; } = String.Empty;
        public List<CustomerLocation> locations { get; set; } = new List<CustomerLocation>();
    }
    public class CustomerDetails
    {
        public List<Customers> customers { get; set; } = new List<Customers>();
    }
}
