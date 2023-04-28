using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models.Entities;


namespace MatterManagementWebApp.Services.Data.Mappers
{
    public class MapMatterClient
    {
        public MatterClientDto Map(Matter entity)
        {
            return new MatterClientDto
            {
                Id = entity.MatterId,
                MatterName = entity.FullName,
                OpenDate = entity.OpenDate,
                CloseDate = entity.CloseDate,
                ClientName = entity.Client.FullName,
                JurisdictionArea = entity.Jurisdiction.Area,
                BillingAttorneyName = entity.BillingAttorney.FullName,
                ResponsibleAttorneyName = entity.ResponsibleAttorney.FullName
            };
        }
    }
}
