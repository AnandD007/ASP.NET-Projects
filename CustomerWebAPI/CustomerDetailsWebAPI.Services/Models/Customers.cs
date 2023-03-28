namespace CustomerWebAPI.Services.Models
{
    public class Customers
    {
        /// <summary>
        /// Customer Id Details
        /// </summary>
        public int CustomerId { get; set; }
        public List<CustomerLocation> CustomerLocation { get; set; } 
    }
}