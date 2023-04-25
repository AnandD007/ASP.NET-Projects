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
        void Add(ClientDto client);
        IEnumerable<ClientDto> GetAll();
        ClientDto GetById(int ClientId);
        void Update(ClientDto client);
        void Delete(int ClientId);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly MatterDBContext _context;

        public ClientRepository(MatterDBContext context)
        {
            _context = context;
        }

        public void Add(ClientDto client)
        {
            var entity = new Client
            {
                ClientId = client.ClientId,
                FullName = client.FullName,
                PhoneNo = client.PhoneNo,
                EmailId = client.EmailId,
                Gender = client.Gender,
                Age = client.Age
            };
            _context.Clients.Add(entity);
            _context.SaveChanges();
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

        public void Update(ClientDto client)
        {
            var entity = _context.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            entity.FullName = client.FullName;
            entity.PhoneNo = client.PhoneNo;
            entity.EmailId = client.EmailId;
            entity.Gender = client.Gender;
            entity.Age = client.Age;

            _context.SaveChanges();
        }

        public void Delete(int ClientId)
        {
            var entity = _context.Clients.SingleOrDefault(c => c.ClientId == ClientId);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            _context.Clients.Remove(entity);
            _context.SaveChanges();
        }
    }
}
