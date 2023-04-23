using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebApp.Services.Data.Entities;

[Table("Customer")]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(30)]
    public string? LastName { get; set; }
    [Required]
    public string? PhoneNo { get; set; }
    [Required]
    public string? EmailId { get; set; }
    [MaxLength(50)]
    public string? StreetName { get; set; }

    [MaxLength(50)]
    public string? Landmark { get; set; }

    [MaxLength(25)]
    public string? City { get; set; }
    [MaxLength(25)]
    public string? State { get; set; }
    [MaxLength(25)]
    public string? Country { get; set; }
    [MaxLength(25)]
    public string? Zipcode { get; set; }
}


