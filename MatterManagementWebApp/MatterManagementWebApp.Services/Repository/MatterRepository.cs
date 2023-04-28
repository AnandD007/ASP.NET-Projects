using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Data.Mappers;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IMatterRepository
    {
        int Add(MatterDto matter);
        int Update(MatterDto matter);
        int Delete(int id);
        MatterDto GetById(int id);
        List<MatterDto> GetAll();
        List<MatterClientDto> GetMattersForClient(int clientId);
        List<InvoiceAttorneyDto> GetLastWeeksBillingByAttorneys();
        decimal GetLastWeeksBillingForAttorney(int AttorneyId);
        List<IGrouping<int, MatterClientDto>> GetAllMattersByClients();
        List<InvoiceMatterDto> GetInvoicesForMatter(int matterId);
        List<IGrouping<int, InvoiceMatterDto>> GetAllInvoicesByMatters();

    }

    public class MatterRepository : IMatterRepository
    {
        public MatterDBContext _context;


        public MatterRepository(MatterDBContext context)
        {
            _context = context;
        }

        public int Add(MatterDto matter)
        {
            Attorney billingAttorneyCheck = _context.Attorneys.FirstOrDefault(a => a.AttorneyId == matter.BillingAttorneyId);
            Attorney responsibleAttorneyCheck = _context.Attorneys.FirstOrDefault(a => a.AttorneyId == matter.ResponsibleAttorneyId);

            if (billingAttorneyCheck!.JurisdictionId != matter.JurisdictionId)
            {
                return (-1);
            }
            Matter newMatter = new Matter();
            {
                newMatter.FullName = matter.FullName;
                newMatter.OpenDate = matter.OpenDate;
                newMatter.CloseDate = matter.CloseDate;
                newMatter.JurisdictionId = matter.JurisdictionId;
                newMatter.ClientId = matter.ClientId;
                newMatter.BillingAttorneyId = matter.BillingAttorneyId;
                newMatter.ResponsibleAttorneyId = matter.ResponsibleAttorneyId;
            }
            _context.Matters.Add(newMatter);
            _context.SaveChanges();
            return newMatter.MatterId;
        }

        public int Update(MatterDto matter)
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
                return entity.MatterId;
                }
                else { return 0; }
        }

        public int Delete(int id)
        {
            var entity = _context.Matters.Find(id);
            if (entity != null)
            {
                _context.Matters.Remove(entity);
                _context.SaveChanges();
                return entity.MatterId;
            }
            else
            {
                return 0;
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
        public List<MatterClientDto> GetMattersForClient(int clientId)
        {
            IQueryable<Matter> mattersByClient = _context.Matters
            .Include(m => m.BillingAttorney)
            .Include(m => m.ResponsibleAttorney)
            .Include(m => m.Jurisdiction)
            .Include(m => m.Client)
            .Where(c => c.ClientId.Equals(clientId));

            return mattersByClient.Select(c => new MapMatterClient().Map(c)).ToList();
        }
        public List<IGrouping<int, InvoiceMatterDto>> GetAllInvoicesByMatters()
        {
            List<IGrouping<int, InvoiceMatterDto>> invoices = _context.Invoices
                .Include(m => m.Matter)
                .Include(m => m.Attorney)
                .Select(c => new MapInvoiceMatter().Map(c)).AsEnumerable()
                .GroupBy(s => s.MatterId).ToList();
            return invoices;
        }
        public List<InvoiceMatterDto> GetInvoicesForMatter(int matterId)
        {
            IQueryable<Invoice> invoicesForMatter = _context.Invoices
            .Include(m => m.Matter)
            .Include(m => m.Attorney)
            .Where(c => c.MatterId.Equals(matterId));

            return invoicesForMatter.Select(c => new MapInvoiceMatter().Map(c)).ToList();
        }

        public List<InvoiceAttorneyDto> GetLastWeeksBillingByAttorneys()
        {
            List<int> idList = _context.Attorneys.Where(a => a.AttorneyId != 0).Select(a => a.AttorneyId).ToList();
            int attorneyCount = idList.Count();
            List<decimal> billings = new List<decimal>();

            for (int i = 0; i < attorneyCount; i++)
                billings.Add(GetLastWeeksBillingForAttorney(idList[i]));

            List<InvoiceAttorneyDto> billingByAttorneys = (from a in _context.Attorneys
                                                           select new InvoiceAttorneyDto()
                                                           {
                                                               Id = a.AttorneyId,
                                                               AttorneyName = a.FullName,
                                                               Billing = 0
                                                           }).ToList();

            for (int i = 0; i < attorneyCount; i++)
                billingByAttorneys[i].Billing = (double)billings[i];

            return billingByAttorneys;
        }
            public decimal GetLastWeeksBillingForAttorney(int attorneyId)
        {
            DateTime lastWeekStart = DateTime.Today.AddDays(-7);
            DateTime lastWeekEnd = DateTime.Today.AddDays(-1);
            Attorney attorney = new Attorney();
            decimal totalBilling = _context.Invoices
                .Where(i => i.AttorneyId == attorneyId &&
                            i.Date >= lastWeekStart && i.Date <= lastWeekEnd)
                .Sum(i => attorney.HourlyRate * i.HoursWorked);

            return totalBilling;

        }
        public List<IGrouping<int, MatterClientDto>> GetAllMattersByClients()
        {            
            List<IGrouping<int, MatterClientDto>> Matters = _context.Matters
                .Include(m => m.BillingAttorney)
                .Include(m => m.ResponsibleAttorney)
                .Include(m => m.Jurisdiction)
                .Include(m => m.Client)
                .Select(c => new MapMatterClient().Map(c)).AsEnumerable().GroupBy(s => s.ClientId).ToList();
            return Matters;
        }

    }

}
