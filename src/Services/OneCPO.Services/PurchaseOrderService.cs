using OneCPO.Data.Common.Contracts;
using OneCPO.Data.Models;
using OneCPO.Data.Models.Enums;
using OneCPO.Services.Contracts;
using OneCPO.ViewModels.Input.PurchaseOrder;
using System.Collections.Generic;
using System.Linq;

namespace OneCPO.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IRepository<PurchaseOrder> purchaseOrderRepository;
        private readonly IRepository<Customer> customerRepository;

        public PurchaseOrderService(IRepository<PurchaseOrder> purchaseOrderRepository, IRepository<Customer> customerRepository)
        {
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.customerRepository = customerRepository;
        }

        public IEnumerable<PurchaseOrder> GetAllPurchases()
        {
            var purchases = this.purchaseOrderRepository.All()
                .Where(x => x.Status != StatusType.Deleted && x.Customer.Status != StatusType.Deleted)
                .Select(x => new PurchaseOrder
                {
                    Id = x.Id,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Status = x.Status,
                    TotalAmount = x.TotalAmount,
                    CustomerId = x.CustomerId,
                    Customer = x.Customer
                })
                .ToList();

            return purchases;
        }

        public IEnumerable<PurchaseOrder> GetPurchasesByUserId(int id)
        {
            var purchases = this.purchaseOrderRepository.All()
                .Where(x => x.CustomerId == id && x.Status != StatusType.Deleted)
                .ToList();

            return purchases;
        }

        public ICollection<Customer> GetCustomers()
        {
            var customers = this.customerRepository.All()
                .Where(x => x.Status != StatusType.Deleted && x.Status != StatusType.Inactive)
                .ToList();

            return customers;
        }

        public void AddPurchase(CreatePurchaseOrderModel input)
        {
            var purchase = new PurchaseOrder
            {
                Description = input.Description,
                Price = input.Price,
                Quantity = input.Quantity,
                TotalAmount = input.Price * input.Quantity,
                CustomerId = input.Customer
            };

            this.purchaseOrderRepository.Add(purchase);
            this.purchaseOrderRepository.SaveChanges();
        }

        public void Activate(int id)
        {
            var purchase = GetSinglePurchase(id);

            if (purchase.Status != StatusType.Active && purchase.Status != StatusType.Deleted)
            {
                purchase.Status = StatusType.Active;
                this.purchaseOrderRepository.SaveChanges();
            }
        }

        public void Deactivate(int id)
        {
            var customer = GetSinglePurchase(id);

            if (customer.Status != StatusType.Inactive && customer.Status != StatusType.Deleted)
            {
                customer.Status = StatusType.Inactive;
                this.purchaseOrderRepository.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var purchase = GetSinglePurchase(id);

            purchase.Status = StatusType.Deleted;
            this.purchaseOrderRepository.SaveChanges();
        }

        private PurchaseOrder GetSinglePurchase(int id)
        {
            return this.purchaseOrderRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}