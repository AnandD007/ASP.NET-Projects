using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using MatterManagementWebApp.Services.Repository;
using System;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IInvoiceRepository
    {
        int Add(InvoiceDto invoice);
        IEnumerable<InvoiceDto> GetAll();
        InvoiceDto GetById(int id);
        int Update(int invoiceId, InvoiceDto invoice);
        int Delete(int id);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly MatterDBContext _context;

        public object AttorneyId { get; private set; }

        public InvoiceRepository(MatterDBContext context)
        {
            _context = context;
        }

        public int Add(InvoiceDto invoice)
        {
            Matter currentMatter = _context.Matters.FirstOrDefault(m => m.MatterId == invoice.MatterId);
            Attorney attorneyCheck = _context.Attorneys.FirstOrDefault(a =>
                                (a.AttorneyId == currentMatter.BillingAttorneyId) || (a.AttorneyId == currentMatter.ResponsibleAttorneyId));
            if (attorneyCheck == null)
            {
                return (0);
            }

            int attorneyRate = _context.Attorneys.Where(a => a.AttorneyId == invoice.AttorneyId).Select(a => a.HourlyRate).First();

            Invoice newInvoice = new Invoice();
            {
                newInvoice.Date = invoice.Date;
                newInvoice.HoursWorked = invoice.HoursWorked;
                newInvoice.TotalAmount = (int)invoice.HoursWorked * attorneyRate;
                newInvoice.MatterId = invoice.MatterId;
                newInvoice.AttorneyId = invoice.AttorneyId;
            }
            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();
            return newInvoice.InvoiceId;
        }

        public IEnumerable<InvoiceDto> GetAll()
        {
            return _context.Invoices
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    Date = i.Date,
                    TotalAmount = i.TotalAmount,
                    HoursWorked = i.HoursWorked,
                    MatterId = i.MatterId
                })
                .ToList();
        }

        public InvoiceDto GetById(int id)
        {
            return _context.Invoices
                .Where(i => i.InvoiceId == id)
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.InvoiceId,
                    Date = i.Date,
                    TotalAmount = i.TotalAmount,
                    HoursWorked = i.HoursWorked,
                    MatterId = i.MatterId
                })
                .SingleOrDefault();
        }

        public int Update(int invoiceId, InvoiceDto invoice)
        {
            var entity = _context.Invoices.SingleOrDefault(i => i.InvoiceId == invoiceId);

            int attorneyHourlyRate = _context.Attorneys.Where(a => a.AttorneyId == invoice.AttorneyId).Select(a => a.HourlyRate).First();

            if (entity == null)
                return 0;

            entity.TotalAmount = invoice.TotalAmount;
            entity.Date = invoice.Date;
            entity.HoursWorked = invoice.HoursWorked * attorneyHourlyRate;
            entity.MatterId = invoice.MatterId;
            entity.AttorneyId = invoice.AttorneyId;

            _context.SaveChanges();
            return entity.InvoiceId;
        }

        public int Delete(int id)
        {
            var entity = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);

            if (entity == null)
                return 0;

            _context.Invoices.Remove(entity);
            _context.SaveChanges();
            return entity.InvoiceId;
        }
    }

}
