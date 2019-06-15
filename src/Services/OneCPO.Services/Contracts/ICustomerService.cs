using OneCPO.Data.Models;
using OneCPO.ViewModels.Input.Customer;
using System.Linq;

namespace OneCPO.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<Customer> Sort(string sortOrder, IQueryable<Customer> customers);

        IQueryable<Customer> GetAll();

        Customer GetSingleCustomer(int id);

        int Create(CreateCustomerModel model);

        void EditCustomer(Customer input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}