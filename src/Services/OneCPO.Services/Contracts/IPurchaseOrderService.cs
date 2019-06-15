using OneCPO.Data.Models;
using OneCPO.ViewModels.Input.PurchaseOrder;
using System.Collections.Generic;

namespace OneCPO.Services.Contracts
{
    public interface IPurchaseOrderService
    {
        IEnumerable<PurchaseOrder> GetPurchasesByUserId(int id);

        ICollection<Customer> GetCustomers();

        IEnumerable<PurchaseOrder> GetAllPurchases();

        void AddPurchase(CreatePurchaseOrderModel input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}