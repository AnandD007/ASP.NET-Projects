using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IMatterRepository
    {
        void Add(MatterDto matter);
        void Update(MatterDto matter);
        void Delete(int id);
        MatterDto GetById(int id);
        List<MatterDto> GetAll();
        List<MatterDto> GetMattersByClient(int clientId);
        List<InvoiceDto> GetInvoicesByMatter(int matterId, InvoiceDto invoice);
        List<InvoiceDto> GetLastWeeksBillingByAttorney(int attorneyId, InvoiceDto invoice);
        decimal GetLastWeeksBillingForAttorney(Attorney attorney);
        List<IGrouping<int, MatterDto>> GetAllMattersByClients(MatterDto matter);
        List<IGrouping<int, InvoiceDto>> GetAllInvoicesByMatters(InvoiceDto invoice);




    }

    public class MatterRepository : IMatterRepository
    {
        public MatterDBContext _context;


        public MatterRepository(MatterDBContext context)
        {
            _context = context;
        }

        public void Add(MatterDto matter)
        {
            Attorney? attorney = _context.Attorneys.Where(_ => _.AttorneyId == matter.BillingAttorneyId).FirstOrDefault();
            if (attorney?.JurisdictionId == matter.JurisdictionId)
            {
                var entity = new Matter
                {
                    MatterId = matter.MatterId,
                    FullName = matter.FullName,
                    OpenDate = matter.OpenDate,
                    CloseDate = matter.CloseDate,
                    JurisdictionId = matter.JurisdictionId,
                    BillingAttorneyId = matter.BillingAttorneyId,
                    ResponsibleAttorneyId = matter.ResponsibleAttorneyId,
                    ClientId = matter.ClientId
                };
                _context.Matters.Add(entity);
                _context.SaveChanges();
            }
        }

        public void Update(MatterDto matter)
        {
                var entity = _context.Matters.Find(matter.MatterId);
                if (entity != null)
                {
                    entity.FullName = matter.FullName;
                    entity.OpenDate = matter.OpenDate;
                    entity.CloseDate = matter.CloseDate;
                    entity.JurisdictionId = matter.JurisdictionId;
                    entity.BillingAttorneyId = matter.BillingAttorneyId;
                    entity.ResponsibleAttorneyId = matter.ResponsibleAttorneyId;
                    entity.ClientId = matter.ClientId;
                    _context.SaveChanges();
                }
        }

        public void Delete(int id)
        {
            var entity = _context.Matters.Find(id);
            if (entity != null)
            {
                _context.Matters.Remove(entity);
                _context.SaveChanges();
            }
        }

        public MatterDto GetById(int id)
        {
            var entity = _context.Matters?.Find(id);
            if (entity != null)
            {
                return new MatterDto
                {
                    MatterId = entity.MatterId,
                    FullName = entity.FullName,
                    OpenDate = entity.OpenDate,
                    CloseDate = entity.CloseDate,
                    JurisdictionId = entity.JurisdictionId,
                    BillingAttorneyId = entity.BillingAttorneyId,
                    ResponsibleAttorneyId = entity.ResponsibleAttorneyId,
                    ClientId = entity.ClientId
                };
            }
            return null;
        }

        public List<MatterDto> GetAll()
        {
            return _context.Matters.Select(m => new MatterDto
            {
                MatterId = m.MatterId,
                FullName = m.FullName,
                OpenDate = m.OpenDate,
                CloseDate = m.CloseDate,
                JurisdictionId = m.JurisdictionId,
                BillingAttorneyId = m.BillingAttorneyId,
                ResponsibleAttorneyId = m.ResponsibleAttorneyId,
                ClientId = m.ClientId
            }).ToList();
        }
        public List<MatterDto> GetMattersByClient(int clientId)
        {
            return GetAll()
                .Where(m => m.ClientId == clientId)
                .ToList();
        }

        public List<InvoiceDto> GetInvoicesByMatter(int matterId, InvoiceDto invoice)
        {
            List<Invoice> invoicesByMatter = _context.Invoices.Where(_ => _.MatterId == matterId).ToList();
            return invoicesByMatter.Select(c => invoice).ToList();
        }

        public List<InvoiceDto> GetLastWeeksBillingByAttorney(int attorneyId,InvoiceDto invoice)
        {
            DateTime lastMonday = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek);
            DateTime lastSunday = lastMonday.AddDays(-6);

            List<Invoice> invoices = _context.Invoices
                .Where(i => i.AttorneyId == attorneyId && i.Date >= lastSunday && i.Date <= lastMonday).ToList();
            return invoices.Select(c => invoice).ToList();
        }
        public decimal GetLastWeeksBillingForAttorney(Attorney attorney)
        {
            DateTime lastWeekStart = DateTime.Today.AddDays(-7);
            DateTime lastWeekEnd = DateTime.Today.AddDays(-1);

            decimal totalBilling = _context.Invoices
                .Where(i => i.AttorneyId == attorney.AttorneyId &&
                            i.Date >= lastWeekStart && i.Date <= lastWeekEnd)
                .Sum(i => attorney.HourlyRate * i.HoursWorked);

            return totalBilling;

        }
        public List<IGrouping<int, MatterDto>> GetAllMattersByClients(MatterDto matter)
        {
            List<IGrouping<int, MatterDto>> matters = _context.Matters.Select(c => matter).GroupBy(s => s.ClientId).ToList();
            return matters;
        }

        public List<IGrouping<int, InvoiceDto>> GetAllInvoicesByMatters(InvoiceDto invoice)
        {
            List<IGrouping<int, InvoiceDto>> invoices = _context.Invoices.Select(c => invoice).GroupBy(s => s.MatterId).ToList();
            return invoices;
        }

    }

}
