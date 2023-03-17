namespace CustomerWebAPI.Services.Models
{
    public class CustomerLocation
    {
        /// <summary>
        /// Customers CustomerLocation
        /// </summary>
        public int LocationId { get; set; }
        public string Landmark { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;


    }
}
