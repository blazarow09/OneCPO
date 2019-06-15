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

        public IQueryable<PurchaseOrder> GetAllPurchases()
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
                }).AsQueryable();

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

        public IQueryable<PurchaseOrder> Sort(string sortOrder, IQueryable<PurchaseOrder> purchases)
        {
            switch (sortOrder)
            {
                case "description_desc":
                    purchases = purchases.OrderByDescending(s => s.Description);
                    break;

                case "price_desc":
                    purchases = purchases.OrderByDescending(s => s.Price);
                    break;

                case "qty_desc":
                    purchases = purchases.OrderByDescending(s => s.Quantity);
                    break;

                case "status_desc":
                    purchases = purchases.OrderByDescending(s => s.Status);
                    break;

                case "amount_desc":
                    purchases = purchases.OrderByDescending(s => s.TotalAmount);
                    break;

                case "date_desc":
                    purchases = purchases.OrderByDescending(s => s.CreatedOn);
                    break;

                case "cust_desc":
                    purchases = purchases.OrderByDescending(s => s.Customer.FirstName);
                    break;

                case "Description":
                    purchases = purchases.OrderBy(s => s.Description);
                    break;

                case "Price":
                    purchases = purchases.OrderBy(s => s.Price);
                    break;

                case "Qty":
                    purchases = purchases.OrderBy(s => s.Quantity);
                    break;

                case "Status":
                    purchases = purchases.OrderBy(s => s.Status);
                    break;

                case "Amount":
                    purchases = purchases.OrderBy(s => s.TotalAmount);
                    break;

                case "Date":
                    purchases = purchases.OrderBy(s => s.CreatedOn);
                    break;

                case "Cust":
                    purchases = purchases.OrderBy(s => s.Customer.FirstName);
                    break;

                default:
                    purchases = purchases.OrderBy(s => s.Description);
                    break;
            }

            return purchases;
        }

        private PurchaseOrder GetSinglePurchase(int id)
        {
            return this.purchaseOrderRepository.All()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}