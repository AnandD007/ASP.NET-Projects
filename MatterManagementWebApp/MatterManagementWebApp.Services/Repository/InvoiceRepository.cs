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
        void Add(InvoiceDto invoice);
        IEnumerable<InvoiceDto> GetAll();
        InvoiceDto GetById(int id);
        void Update(InvoiceDto invoice);
        void Delete(int id);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly MatterDBContext _context;

        public object AttorneyId { get; private set; }

        public InvoiceRepository(MatterDBContext context)
        {
            _context = context;
        }

        public void Add(InvoiceDto i)
        {
            var entity = new Invoice
            {
                Date = i.Date,
                TotalAmount = i.TotalAmount,
                HoursWorked = i.HoursWorked,
                MatterId = i.MatterId
            };

            _context.Invoices.Add(entity);
            _context.SaveChanges();

            i.InvoiceId = entity.InvoiceId;
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

        public void Update(InvoiceDto invoice)
        {
            var entity = _context.Invoices.SingleOrDefault(i => i.InvoiceId == invoice.InvoiceId);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            entity.TotalAmount = invoice.TotalAmount;
            entity.Date = invoice.Date;
            entity.HoursWorked = invoice.HoursWorked;
            entity.MatterId = invoice.MatterId;
            entity.AttorneyId = invoice.AttorneyId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            _context.Invoices.Remove(entity);
            _context.SaveChanges();
        }
    }

}
