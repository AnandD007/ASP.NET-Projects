using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Attorney")]
    public class Attorney
    {
        [Key]
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
        public virtual Jurisdiction? Jurisdiction { get; set; }

    }
}
