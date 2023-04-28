using MatterManagementWebApp.Services.Data.DBContext;
using MatterManagementWebApp.Services.Data.DTOs;
using MatterManagementWebApp.Services.Models;
using MatterManagementWebApp.Services.Models.Entities;
using MatterManagementWebApp.Services.Repository;
using System;

namespace MatterManagementWebApp.Services.Repository
{
    public interface IClientRepository
    {
        int Add(ClientDto client);
        IEnumerable<ClientDto> GetAll();
        ClientDto GetById(int ClientId);
        int Update(ClientDto client);
        int Delete(int ClientId);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly MatterDBContext _context;

        public ClientRepository(MatterDBContext context)
        {
            _context = context;
        }

        public int Add(ClientDto client)
        {
            var entity = new Client
            {
                FullName = client.FullName,
                PhoneNo = client.PhoneNo,
                EmailId = client.EmailId,
                Gender = client.Gender,
                Age = client.Age
            };
            _context.Clients.Add(entity);
            _context.SaveChanges();
            return entity.ClientId;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            return _context.Clients
                .Select(c => new ClientDto
                {
                    ClientId = c.ClientId,
                    FullName = c.FullName,
                    PhoneNo = c.PhoneNo,
                    EmailId = c.EmailId,
                    Gender = c.Gender,
                    Age = c.Age
                })
                .ToList();
        }

        public ClientDto GetById(int ClientId)
        {
            return _context.Clients
                .Where(c => c.ClientId == ClientId)
                .Select(c => new ClientDto
                {
                    ClientId = c.ClientId,
                    FullName = c.FullName,
                    PhoneNo = c.PhoneNo,
                    EmailId = c.EmailId,
                    Gender = c.Gender,
                    Age = c.Age
                })
                .SingleOrDefault();
        }

        public int Update(ClientDto client)
        {
            var entity = _context.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);

            if (entity == null)
                return 0;

            entity.FullName = client.FullName;
            entity.PhoneNo = client.PhoneNo;
            entity.EmailId = client.EmailId;
            entity.Gender = client.Gender;
            entity.Age = client.Age;

            _context.SaveChanges();
            return entity.ClientId;
        }

        public int Delete(int ClientId)
        {
            var entity = _context.Clients.SingleOrDefault(c => c.ClientId == ClientId);

            if (entity == null)
                return 0;

            _context.Clients.Remove(entity);
            _context.SaveChanges();
            return entity.ClientId;
        }
    }
}
