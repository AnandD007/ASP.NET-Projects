using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using System;
using System.Data;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IAttorneyRepository
    {
        void Add(AttorneyDto attorney);
        IEnumerable<AttorneyDto> GetAll();
        AttorneyDto GetById(int id);
        void Update(AttorneyDto attorney);
        void Delete(int id);
    }

    public class AttorneyRepository : IAttorneyRepository
    {
        private readonly MatterDBContext _context;

        public AttorneyRepository(MatterDBContext context)
        {
            _context = context;
        }

        public void Add(AttorneyDto a)
        {
            var entity = new Attorney
            {
                AttorneyId = a.AttorneyId,
                FullName = a.FullName,
                EmailId = a.EmailId,
                HourlyRate = a.HourlyRate,
                PhoneNo = a.PhoneNo,
                Role = a.Role,
                JurisdictionId = a.JurisdictionId
            };

            _context.Attorneys.Add(entity);
            _context.SaveChanges();

            a.AttorneyId = entity.AttorneyId;
        }

        public IEnumerable<AttorneyDto> GetAll()
        {
            return _context.Attorneys
                .Select(a => new AttorneyDto
                {
                    AttorneyId = a.AttorneyId,
                    FullName = a.FullName,
                    EmailId = a.EmailId,
                    HourlyRate = a.HourlyRate,
                    PhoneNo = a.PhoneNo,
                    Role = a.Role,
                    JurisdictionId = a.JurisdictionId
                })
                .ToList();
        }

        public AttorneyDto GetById(int id)
        {
            return _context.Attorneys
                .Where(a => a.AttorneyId == id)
                .Select(a => new AttorneyDto
                {
                    AttorneyId = a.AttorneyId,
                    FullName = a.FullName,
                    EmailId = a.EmailId,
                    HourlyRate = a.HourlyRate,
                    PhoneNo = a.PhoneNo,
                    Role = a.Role,
                    JurisdictionId = a.JurisdictionId
                })
                .SingleOrDefault();
        }

        public void Update(AttorneyDto a)
        {
            var entity = _context.Attorneys.SingleOrDefault(a => a.AttorneyId == a.AttorneyId);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");
            entity.FullName = a.FullName;
            entity.EmailId = a.EmailId;
            entity.HourlyRate = a.HourlyRate;
            entity.PhoneNo = a.PhoneNo;
            entity.Role = a.Role;
            entity.JurisdictionId = a.JurisdictionId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Attorneys.SingleOrDefault(a => a.AttorneyId == id);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            _context.Attorneys.Remove(entity);
            _context.SaveChanges();
        }
    }

}
