using CustomerWebApp.Services.Data.DBContext;
using CustomerWebApp.Services.Data.Entities;

namespace CustomerWebApp.Services.Repository
{
    public interface ICustomer
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int CustomerId);
        int AddCustomer(Customer customer);
        int UpdateCustomer(int CustomerId, Customer updatedCustomer);
        int DeleteCustomer(int CustomerId);
    }

    public class CustomerRepository : ICustomer
    {
        private CustomerDBContext _context;
        public CustomerRepository(CustomerDBContext newContext)
        {
            _context = newContext;
        }

        public List<Customer> GetAllCustomer()
        {
            return _context.Customer.ToList();
        }
        public Customer GetCustomer(int CustomerId)
        {
            return _context.Customer.FirstOrDefault(c => c.CustomerId == CustomerId);
        }
        public int AddCustomer(Customer customer)
        {
            Customer result = _context.Customer.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (result == null)
            {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return customer.CustomerId;
            }
            return -1;
        }
        public int UpdateCustomer(int CustomerId, Customer updatedCustomer)
        {
            Customer customer = _context.Customer.FirstOrDefault(c => c.CustomerId == CustomerId);
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.PhoneNo = updatedCustomer.PhoneNo;
                customer.EmailId = updatedCustomer.EmailId;
                customer.StreetName = updatedCustomer.StreetName;
                customer.Landmark = updatedCustomer.Landmark;
                customer.City = updatedCustomer.City;
                customer.State = updatedCustomer.State;
                customer.Country = updatedCustomer.Country;
                customer.Zipcode = updatedCustomer.Zipcode;
                _context.SaveChanges();
                return customer.CustomerId;
            }
            return -1;
        }
        public int DeleteCustomer(int CustomerId)
        {
            Customer customer = _context.Customer.FirstOrDefault(c => c.CustomerId == CustomerId);
            if (string.IsNullOrEmpty(customer.StreetName) &&
                 string.IsNullOrEmpty(customer.Landmark) &&
                 string.IsNullOrEmpty(customer.City) &&
                 string.IsNullOrEmpty(customer.Zipcode) &&
                 string.IsNullOrEmpty(customer.Country) && 
                 string.IsNullOrEmpty(customer.State))
            {
                _context.Customer.Remove(customer);
                _context.SaveChanges();
                return customer.CustomerId;
            }
            else
                return 0;
        }
    }
}
