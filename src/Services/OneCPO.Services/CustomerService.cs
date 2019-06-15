using OneCPO.Data.Common.Contracts;
using OneCPO.Data.Models;
using OneCPO.Data.Models.Enums;
using OneCPO.Services.Contracts;
using OneCPO.Services.Mapping;
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

        public IQueryable<CustomersViewModel> GetAll()
        {
            var customers = this.customerRepository.All().To<CustomersViewModel>();

            return customers;
        }

        public async Task<int> Create(Customer model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Telephone = model.Telephone
            };

            await this.customerRepository.AddAsync(customer);
            await this.customerRepository.SaveChangesAsync();

            return model.Id;
        }

        public void Activate(int id)
        {
            var customer = GetSingleCustomer(id);

            if (customer.Status != StatusType.Active && customer.Status != StatusType.Deleted)
            {
                customer.Status = StatusType.Active;
                this.customerRepository.SaveChangesAsync();
            }
        }

        public void Deactivate(int id)
        {
            var customer = GetSingleCustomer(id);

            if (customer.Status != StatusType.Inactive && customer.Status != StatusType.Deleted)
            {
                customer.Status = StatusType.Inactive;
                this.customerRepository.SaveChangesAsync();
            }
        }

        public void DeleteUser(int id)
        {
            var customer = GetSingleCustomer(id);

            customer.Status = StatusType.Deleted;
            this.customerRepository.SaveChangesAsync();
        }

        public void EditCustomer(Customer input)
        {
            var customer = this.GetSingleCustomer(input.Id);

            customer.FirstName = input.FirstName;
            customer.LastName = input.LastName;
            customer.Gender = input.Gender;
            customer.Telephone = input.Telephone;

            this.customerRepository.SaveChangesAsync();
        }

        private Customer GetSingleCustomer(int id)
        {
            return this.customerRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}