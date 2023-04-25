using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Jurisdiction")]
    public class Jurisdiction
    {
        [Key]
        public int JurisdictionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }
        [Required]
        [MaxLength(20)]
        public string? PhoneNo { get; set; }
        [Required]
        public string? EmailId { get; set; }
        public ICollection<Attorney>? Attorneys { get; set; }
        public ICollection<Matter>? Matters { get; set; }


    }
}
