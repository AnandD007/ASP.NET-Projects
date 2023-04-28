using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterManagementWebApp.Services.Data.Mappers
{
    public class MapInvoiceMatter
    {
        public InvoiceMatterDto Map(Invoice entity)
        {
            return new InvoiceMatterDto
            {
                Id = entity.InvoiceId,
                Date = entity.Date,
                HoursWorked = entity.HoursWorked,
                TotalAmount = entity.TotalAmount,
                MatterId = entity.MatterId,
                MatterName = entity.Matter.FullName,
                AttorneyName = entity.Attorney.FullName
            };
        }
    }
}
