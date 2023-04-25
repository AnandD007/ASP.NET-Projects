using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class JurisdictionDto
    {
        public int JurisdictionId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailId { get; set; }
    }
}
