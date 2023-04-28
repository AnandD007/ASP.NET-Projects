namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class InvoiceMatterDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public float? TotalAmount { get; set; }
        public int MatterId { get; set; }
        public string? MatterName { get; set; }
        public string? AttorneyName { get; set; }
    }
}
