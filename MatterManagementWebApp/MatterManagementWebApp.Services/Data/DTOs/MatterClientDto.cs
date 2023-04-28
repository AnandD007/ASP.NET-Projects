namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class MatterClientDto
    {
        public int Id {get; set;}
        public string? MatterName {get; set;}
        public DateTime OpenDate { get; set;}
        public DateTime CloseDate { get; set;}
        public int ClientId { get; set;}
        public string? ClientName { get; set;}
        public string? BillingAttorneyName { get; set;}
        public string? ResponsibleAttorneyName { get; set;}
        public string? JurisdictionArea { get; set;}
    }
}
