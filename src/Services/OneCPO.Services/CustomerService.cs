using OneCPO.Data.Common.Contracts;
using OneCPO.Data.Models;
using OneCPO.Data.Models.Enums;
using OneCPO.Services.Contracts;
using OneCPO.ViewModels.Input.Customers;
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

        public int Create(CreateCustomerModel model)
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

        public IQueryable<Customer> Sort(string sortOrder, IQueryable<Customer> customers)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.FirstName);
                    break;

                case "gender_desc":
                    customers = customers.OrderByDescending(s => s.Gender);
                    break;

                case "phone_desc":
                    customers = customers.OrderByDescending(s => s.Telephone);
                    break;

                case "status_desc":
                    customers = customers.OrderByDescending(s => s.Status);
                    break;

                case "date_desc":
                    customers = customers.OrderByDescending(s => s.CreatedOn);
                    break;

                case "Date":
                    customers = customers.OrderBy(s => s.FirstName);
                    break;

                case "Phone":
                    customers = customers.OrderBy(s => s.Telephone);
                    break;

                case "Status":
                    customers = customers.OrderBy(s => s.Status);
                    break;

                case "Gender":
                    customers = customers.OrderBy(s => s.Gender);
                    break;

                default:
                    customers = customers.OrderBy(s => s.FirstName);
                    break;
            }

            return customers;
        }

        private Customer GetSingleCustomer(int id)
        {
            return this.customerRepository.All().FirstOrDefault(x => x.Id == id);
        }
    }
}