using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatterManagementWebApp.Services.Models.Entities
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FullName { get; set; }
        [Required]
        public string? PhoneNo { get; set; }
        [Required]
        public string? EmailId { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        [MaxLength(10)]
        public int? Age { get; set; }
        
        public ICollection<Matter>? Matters { get; set;}
    }
}
