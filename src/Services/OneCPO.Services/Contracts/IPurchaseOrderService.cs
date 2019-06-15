using OneCPO.Data.Models;
using OneCPO.ViewModels.Input.PurchaseOrder;
using System.Collections.Generic;
using System.Linq;

namespace OneCPO.Services.Contracts
{
    public interface IPurchaseOrderService
    {
        IQueryable<PurchaseOrder> Sort(string sortOrder, IQueryable<PurchaseOrder> purchases);

        IEnumerable<PurchaseOrder> GetPurchasesByUserId(int id);

        ICollection<Customer> GetCustomers();

        IQueryable<PurchaseOrder> GetAllPurchases();

        void AddPurchase(CreatePurchaseOrderModel input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}