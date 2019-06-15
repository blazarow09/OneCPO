using OneCPO.Data.Models;
using System.Linq;

namespace OneCPO.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetAll();

        int Create(Customer model);

        void EditCustomer(Customer input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}