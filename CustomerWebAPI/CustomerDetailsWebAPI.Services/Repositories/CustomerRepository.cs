using CustomerWebAPI.Services.Models;

namespace CustomerWebAPI.Services.Repositories
{
    public interface ICustomerLocationsRepository
    {
        public CustomerLocation GetLocationByCustomerId(int customerId, int locationId);
        public CustomerLocation AddCustomerLocation(int customerId, CustomerLocation customerLocation);
        public CustomerLocation UpdateCustomerLocation(int customerId, int locationId, CustomerLocation customerLocation);
        public CustomerLocation DeleteCustomerLocation(int customerId, int locationId);
    }
    public interface ICustomersRepository
    {
        public IEnumerable<Customers> GetAllCustomers();
        public Customers GetCustomerById(int CustomerId);
        public Customers AddCustomer(Customers CustomerId);
        public Customers UpdateCustomer(int customerId, Customers CustomerId);
        public Customers DeleteCustomer(int CustomerId);

    }

    public class CustomerRepository : ICustomersRepository, ICustomerLocationsRepository
    {
        private static List<Customers>? customers;

        public CustomerRepository()
        {
            customers = new List<Customers>();
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return customers;
        }
        public Customers GetCustomerById(int customerId)
        {
            Customers customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
            {
                return customer;
            }
            return null;
        }
        public Customers AddCustomer(Customers value)
        {
            value.CustomerId = customers!.Count > 0 ? customers.Max(customers => customers.CustomerId) + 1 : 1;
            Console.WriteLine("customers: " + value);
            if (value != null)
            {
                customers.Add(value);
                return value;
            }
            customers.Add(value);
            return value;
        }
        public Customers UpdateCustomer(int customerId, Customers inputCustomer)
        {
            Customers customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (inputCustomer.CustomerId != null)
            {
                customer.CustomerId = inputCustomer.CustomerId;
                if (inputCustomer.CustomerLocation != null && inputCustomer.CustomerLocation.Any())
                {
                    customer.CustomerLocation = inputCustomer.CustomerLocation;
                }
                return customer;
            }
            return null;
        }

        public Customers DeleteCustomer(int customerId)
        {
            var customer = customers.FirstOrDefault(w => w.CustomerId == customerId);
            if (customer.CustomerLocation[0] != null)
            {
                customers.Remove(customer);

                return customer;
            }
            return null;
        }
        public CustomerLocation AddCustomerLocation(int customerId, CustomerLocation customerLocation)
        {
            foreach (Customers customer in customers!)
            {
                if (customer.CustomerId == customerId)
                {
                    if (customer.CustomerLocation == null)
                    {
                        customer.CustomerLocation = new List<CustomerLocation>();
                    }
                    customerLocation.LocationId = customer.CustomerLocation!.Count > 0 ? customer.CustomerLocation.Max(locations => locations.LocationId) + 1 : 1;
                    customer.CustomerLocation.Add(customerLocation);
                    return customerLocation;
                }
            }
            return null;
        }

        public CustomerLocation GetLocationByCustomerId(int customerId, int locationId)
        {
            foreach (Customers customer in customers)
            {
                List<CustomerLocation> customerLocations = new List<CustomerLocation>();

                if (customer.CustomerId == customerId)
                {
                    foreach (CustomerLocation location in customer.CustomerLocation)
                    {
                        if (location.LocationId == locationId)
                        {
                            return location;
                        }
                    }
                }
            }
            return null;
        }

        public CustomerLocation UpdateCustomerLocation(int customerId, int locationId, CustomerLocation location)
        {
            foreach (Customers customer in customers)
            {
                if (customer.CustomerId == customerId)
                {
                    foreach (CustomerLocation newLocation in customer.CustomerLocation)
                    {
                        if (newLocation.LocationId == locationId)
                        {
                            newLocation.LocationId = locationId;
                            newLocation.Landmark = location.Landmark;
                            newLocation.City = location.City;
                            newLocation.State = location.State;
                            newLocation.Country = location.Country;
                            return newLocation;
                        }
                    }
                }
            }
            return null!;
        }

        public CustomerLocation DeleteCustomerLocation(int customerId, int locationId)
        {
            foreach (Customers customer in customers!)
            {
                if (customer.CustomerId == customerId)
                {
                    List<CustomerLocation> addedLocations = new List<CustomerLocation>();
                    foreach (CustomerLocation location in customer.CustomerLocation)
                    {
                        addedLocations.Add(location);
                    }
                    CustomerLocation removeLocations = addedLocations.Single(r => r.LocationId == locationId);
                    if (addedLocations.Contains(removeLocations))
                    {
                        addedLocations.Remove(removeLocations);
                        customer.CustomerLocation = addedLocations;
                        return removeLocations;
                    }
                }
            }
            return null!;
        }
    }

}