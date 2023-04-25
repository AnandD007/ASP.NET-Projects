using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatterManagementWebApp.Services.Models.Entities;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Matter")]
    public class Matter
    {
        [Key]
        public int MatterId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }
        [Required]
        public DateTime? OpenDate { get; set; }
        [Required]
        public DateTime? CloseDate { get; set; }
        [Required]
        [ForeignKey("Jurisdiction")]
        public int JurisdictionId { get; set; }
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [Required]
        public int ResponsibleAttorneyId { get; set; }
        [Required]
        public int BillingAttorneyId { get; set; }
        public virtual Jurisdiction? Jurisdiction { get; set; }
        public virtual Client? Client { get; set; }
        public virtual Attorney? ResponsibleAttorney { get; set; }
        public virtual Attorney? BillingAttorney { get; set; }
        public ICollection<Invoice>? Invoices { get; set;}
}
}


