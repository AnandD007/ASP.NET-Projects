using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailId { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }

    }

}

