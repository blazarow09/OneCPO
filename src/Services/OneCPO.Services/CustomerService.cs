using OneCPO.Data.Common.Contracts;
using OneCPO.Data.Models;
using OneCPO.Data.Models.Enums;
using OneCPO.Services.Contracts;
using OneCPO.ViewModels.Output;
using System.Linq;
using System.Threading.Tasks;

namespace OneCPO.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IQueryable<Customer> GetAll()
        {
            var customers = this.customerRepository.All()
                .Where(x => x.Status != StatusType.Deleted);

            return customers;
        }

        public int Create(Customer model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Telephone = model.Telephone
            };

            this.customerRepository.Add(customer);
            this.customerRepository.SaveChanges();

            return model.Id;
        }

        public void Activate(int id)
        {
            var customer = GetSingleCustomer(id);

            if (customer.Status != StatusType.Active && customer.Status != StatusType.Deleted)
            {
                customer.Status = StatusType.Active;
                this.customerRepository.SaveChanges();
            }
        }

        public void Deactivate(int id)
        {
            var customer = GetSingleCustomer(id);

            if (customer.Status != StatusType.Inactive && customer.Status != StatusType.Deleted)
            {
                customer.Status = StatusType.Inactive;
                this.customerRepository.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var customer = GetSingleCustomer(id);

            customer.Status = StatusType.Deleted;
            this.customerRepository.SaveChanges();
        }

        public void EditCustomer(Customer input)
        {
            var customer = this.GetSingleCustomer(input.Id);

            customer.FirstName = input.FirstName;
            customer.LastName = input.LastName;
            customer.Gender = input.Gender;
            customer.Telephone = input.Telephone;

            this.customerRepository.SaveChanges();
        }

        private Customer GetSingleCustomer(int id)
        {
            return this.customerRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}