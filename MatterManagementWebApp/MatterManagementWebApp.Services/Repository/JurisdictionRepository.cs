using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IJurisdictionRepository
    {
        void Add(JurisdictionDto jurisdiction);
        IEnumerable<JurisdictionDto> GetAll();
        JurisdictionDto GetById(int JurisdictionId);
        void Update(JurisdictionDto jurisdiction);
        void Delete(int JurisdictionId);
    }
    public class JurisdictionRepository : IJurisdictionRepository
    {
        public MatterDBContext _context;

        public JurisdictionRepository(MatterDBContext context)
        {
            _context = context;
        }

        public void Add(JurisdictionDto jurisdiction)
        {
            var entity = new Jurisdiction
            {
                JurisdictionId = jurisdiction.JurisdictionId,
                FullName = jurisdiction.FullName,
                PhoneNo = jurisdiction.PhoneNo,
                EmailId = jurisdiction.EmailId,
            };
            _context.Jurisdictions.Add(entity);
            _context.SaveChanges();
        }

        public void Update(JurisdictionDto jurisdiction)

        {
            var entity = _context.Jurisdictions.Find(jurisdiction.JurisdictionId);
            if (entity != null)
            {
                entity.FullName = jurisdiction.FullName;
                entity.PhoneNo = jurisdiction.PhoneNo;
                entity.EmailId = jurisdiction.EmailId;
                _context.SaveChanges();
            }
        }

        public void Delete(int JurisdictionId)
        {
            var entity = _context.Jurisdictions.Find(JurisdictionId);
            if (entity != null)
            {
                _context.Jurisdictions.Remove(entity);
                _context.SaveChanges();
            }
        }

        public JurisdictionDto GetById(int JurisdictionId)
        {
            var entity = _context.Jurisdictions.Find(JurisdictionId);
            if (entity != null)
            {
                return new JurisdictionDto
                {
                    JurisdictionId = entity.JurisdictionId,
                    PhoneNo = entity.PhoneNo,
                    EmailId = entity.EmailId,
                    FullName = entity.FullName
                };
            }
            return null;
        }


        public IEnumerable<JurisdictionDto> GetAll()
        {
            return _context.Jurisdictions.Select(j => new JurisdictionDto
            {
                JurisdictionId = j.JurisdictionId,
                FullName = j.FullName,
                PhoneNo= j.PhoneNo,
                EmailId = j.EmailId
            }).ToList();
        }
    }

}
