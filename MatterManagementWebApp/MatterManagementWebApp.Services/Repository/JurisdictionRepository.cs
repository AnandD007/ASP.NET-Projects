using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IJurisdictionRepository
    {
        int Add(JurisdictionDto jurisdiction);
        IEnumerable<JurisdictionDto> GetAll();
        JurisdictionDto GetById(int JurisdictionId);
        int Update(JurisdictionDto jurisdiction);
        int Delete(int JurisdictionId);
    }
    public class JurisdictionRepository : IJurisdictionRepository
    {
        public MatterDBContext _context;

        public JurisdictionRepository(MatterDBContext context)
        {
            _context = context;
        }

        public int Add(JurisdictionDto jurisdiction)
        {
            var entity = new Jurisdiction
            {
                JurisdictionId = jurisdiction.JurisdictionId,
                Area = jurisdiction.Area,
                EmailId = jurisdiction.EmailId,
            };
            _context.Jurisdictions.Add(entity);
            _context.SaveChanges();
            return entity.JurisdictionId;
        }

        public int Update(JurisdictionDto jurisdiction)

        {
            var entity = _context.Jurisdictions.Find(jurisdiction.JurisdictionId);
            if (entity != null)
            {
                entity.Area = jurisdiction.Area;
                entity.EmailId = jurisdiction.EmailId;
                _context.SaveChanges();
                return entity.JurisdictionId;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int JurisdictionId)
        {
            var entity = _context.Jurisdictions.Find(JurisdictionId);
            if (entity != null)
            {
                _context.Jurisdictions.Remove(entity);
                _context.SaveChanges();
                return entity.JurisdictionId;
            }
            else { return 0; }
        }

        public JurisdictionDto GetById(int JurisdictionId)
        {
            var entity = _context.Jurisdictions.Find(JurisdictionId);
            if (entity != null)
            {
                return new JurisdictionDto
                {
                    JurisdictionId = entity.JurisdictionId,
                    Area = entity.Area,
                    EmailId = entity.EmailId
                };
            }
            return null;
        }


        public IEnumerable<JurisdictionDto> GetAll()
        {
            return _context.Jurisdictions.Select(j => new JurisdictionDto
            {
                JurisdictionId = j.JurisdictionId,
                Area= j.Area,
                EmailId = j.EmailId
            }).ToList();
        }
    }

}
