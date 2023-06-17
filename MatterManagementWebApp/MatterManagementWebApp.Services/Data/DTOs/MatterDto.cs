namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class MatterDto
    {
        public int MatterId { get; set; }
        public string? FullName { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int ClientId { get; set; }
        public int JurisdictionId { get; set; }
        public int BillingAttorneyId { get; set; }
        public int ResponsibleAttorneyId { get; set; }


    }
}
