using OneCPO.Data.Models;
using OneCPO.ViewModels.Output;
using System.Linq;
using System.Threading.Tasks;

namespace OneCPO.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<CustomersViewModel> GetAll();

        Task<int> Create(Customer model);

        void EditCustomer(Customer input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}