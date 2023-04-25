using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class AttorneyDto
    {
        public int AttorneyId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailId { get; set; }
        public int HourlyRate { get; set; }
        public string? Role { get; set; }
        public int JurisdictionId { get; set; }

    }
}
