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
        int Add(AttorneyDto attorney);
        IEnumerable<AttorneyDto> GetAll();
        List<AttorneyDto> GetAttorniesByJurisdiction(int jurisdictionId);
        AttorneyDto GetById(int id);
        int Update(AttorneyDto attorney);
        int Delete(int id);
    }

    public class AttorneyRepository : IAttorneyRepository
    {
        private readonly MatterDBContext _context;

        public AttorneyRepository(MatterDBContext context)
        {
            _context = context;
        }

        public int Add(AttorneyDto a)
        {
            var entity = new Attorney
            {
                FullName = a.FullName,
                EmailId = a.EmailId,
                HourlyRate = a.HourlyRate,
                PhoneNo = a.PhoneNo,
                Role = a.Role,
                JurisdictionId = a.JurisdictionId
            };

            _context.Attorneys.Add(entity);
            _context.SaveChanges();
            return entity.AttorneyId;

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
        public List<AttorneyDto> GetAttorniesByJurisdiction(int jurisdictionId)
        {
            List<AttorneyDto> attorneys = (from a in _context.Attorneys
                                           where a.JurisdictionId == jurisdictionId
                                           select new AttorneyDto()
                                           {
                                               AttorneyId = a.AttorneyId,
                                               FullName = a.FullName,
                                               EmailId = a.EmailId,
                                               PhoneNo = a.PhoneNo,
                                               Role = a.Role,
                                               HourlyRate = a.HourlyRate,
                                               JurisdictionId = a.JurisdictionId
                                           }).ToList();
            return attorneys;
        }
        public int Update(AttorneyDto a)
        {
            var entity = _context.Attorneys.SingleOrDefault(a => a.AttorneyId == a.AttorneyId);

            if (entity == null)
                return 0;
            else
            {
                entity.FullName = a.FullName;
                entity.EmailId = a.EmailId;
                entity.HourlyRate = a.HourlyRate;
                entity.PhoneNo = a.PhoneNo;
                entity.Role = a.Role;
                entity.JurisdictionId = a.JurisdictionId;
                _context.SaveChanges();
                return entity.AttorneyId;
            }
        }

        public int Delete(int id)
        {
            var entity = _context.Attorneys.SingleOrDefault(a => a.AttorneyId == id);

            if (entity == null)
                return 0;
            else
            {
                _context.Attorneys.Remove(entity);
                _context.SaveChanges();
                return entity.AttorneyId;
            }
        }
    }

}
