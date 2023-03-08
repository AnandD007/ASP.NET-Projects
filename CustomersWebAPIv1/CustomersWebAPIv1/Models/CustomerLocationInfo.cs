
namespace CustomersWebAPIv1
{
    public class CustomerLocationInfo
    {
        /// <summary>
        /// CustomerInfo Location CustomerLocationInfo
        /// </summary>
        public string landmark { get; set; } = String.Empty;
        public string city { get; set; } = String.Empty;
        public string state { get; set; } = String.Empty;
        public string country { get; set; } = String.Empty;
    }
    public class CustomerDetails
    {
        public List<CustomerInfo> customers { get; set; } = new List<CustomerInfo>();
    }
}
