

using System.Text.Json.Serialization;


namespace CustomersWebAPIv1
{
    public class CustomerInfo
    {
        /// <summary>
        /// Customer Id Details
        /// </summary>
        public string customerId { get; set; } = String.Empty;
        public List<CustomerLocationInfo> locations { get; set; } = new List<CustomerLocationInfo>();
    }
}