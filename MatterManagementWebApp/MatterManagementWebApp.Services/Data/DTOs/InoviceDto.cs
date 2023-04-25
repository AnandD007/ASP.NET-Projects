using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Data.DTOs
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public decimal HoursWorked { get; set; }
        public int TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public int MatterId { get; set; }
        public int AttorneyId { get; set; }

    }
}
