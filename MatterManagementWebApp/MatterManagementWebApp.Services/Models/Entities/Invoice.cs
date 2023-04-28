using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key][JsonIgnore]
        public int InvoiceId { get; set; }
        [Required]
        public decimal HoursWorked { get; set; }
        [Required]  
        public int TotalAmount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [ForeignKey("Matter")]
        public int MatterId { get; set; }
        [Required]
        [ForeignKey("Attorney")]
        public int AttorneyId { get; set; }
        public virtual Matter? Matter { get; set; }
        public virtual Attorney? Attorney { get; set; }


    }
}
