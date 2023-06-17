using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Models.Entities;

namespace MatterManagementWebApp.Services.Repository
{

    public class BillingRepository : DbContext
    {
        private readonly MatterDBContext _context;

        public BillingRepository(MatterDBContext context)
        {
            _context = context;
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
    }
}
