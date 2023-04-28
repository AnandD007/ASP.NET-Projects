using MatterManagementWebApp.Services.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Jurisdiction")]
    public class Jurisdiction
    {
        [Key]
        public int JurisdictionId { get; set; }
        [Required][JsonIgnore]
        [MaxLength(20)]
        public string? Area { get; set; }
        [Required]
        public string? EmailId { get; set; }
        public ICollection<Attorney>? Attorneys { get; set; }
        public ICollection<Matter>? Matters { get; set; }


    }
}
