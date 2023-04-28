using MatterManagementWebApp.Services.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MatterManagementWebApp.Services.Models.Entities
{
    public class Attorney
    {
        [Key][JsonIgnore]
        public int AttorneyId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }
        [Required]
        public string? PhoneNo { get; set; }
        [Required]
        public string? EmailId { get; set; }
        [Required]
        public int HourlyRate { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        [ForeignKey("Jurisdiction")]
        public int JurisdictionId { get; set; }
        public ICollection<Matter> BillingAttorneyMatters { get; set; }
        public ICollection<Matter> ResponsibleAttorneyMatters { get; set; }
        public Jurisdiction Jurisdiction { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

    }
}
