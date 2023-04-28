using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatterManagementWebApp.Services.Models.Entities;
using System.Text.Json.Serialization;

namespace MatterManagementWebApp.Services.Models.Entities
{
    public class Matter
    {
        [Key][JsonIgnore]
        public int MatterId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        [Required]
        [ForeignKey("Jurisdiction")]
        public int JurisdictionId { get; set; }
        public Jurisdiction Jurisdiction { get; set; } = null!;
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        [ForeignKey("Attorney")]
        public int BillingAttorneyId { get; set; }
        public Attorney BillingAttorney { get; set; }
        [Required]
        [ForeignKey("Attorney")]
        public int ResponsibleAttorneyId { get; set; }
        public Attorney ResponsibleAttorney { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}


