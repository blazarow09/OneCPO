using Microsoft.AspNetCore.Mvc;
using OneCPO.Data.Models;
using OneCPO.Services.Contracts;
using OneCPO.ViewModels.Input.PurchaseOrder;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneCPO.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }

        public async Task<IActionResult> Index(string sortOrder, int page = 1)
        {
            ViewData["DescriptionSortParm"] = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["QtySortParm"] = sortOrder == "Qty" ? "qty_desc" : "Qty";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewData["DateSortParm"] = sortOrder == "Status" ? "date_desc" : "Date";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["CustSortParm"] = sortOrder == "Cust" ? "cust_desc" : "Cust";

            var purchases = this.purchaseOrderService.GetAllPurchases();

            purchases = this.purchaseOrderService.Sort(sortOrder, purchases);

            var model = await PagingList.CreateAsync(purchases.OrderBy(x => 0), 6, page);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreatePurchaseOrderModel purchase)
        {
            this.purchaseOrderService.AddPurchase(purchase);

            return RedirectToAction("Index", "PurchaseOrder");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var purchase = this.purchaseOrderService.GetSinglePurchase(id);

            return this.View(purchase);
        }

        [HttpPost]
        public IActionResult Edit(PurchaseOrder input)
        {
            this.purchaseOrderService.EditPurchase(input);

            return RedirectToAction("Index", "PurchaseOrder");
        }

        [HttpGet]
        public IActionResult Activate(int id)
        {
            this.purchaseOrderService.Activate(id);

            return RedirectToAction("Index", "PurchaseOrder");
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            this.purchaseOrderService.DeleteUser(id);

            return RedirectToAction("Index", "PurchaseOrder");
        }

        [HttpGet]
        public IActionResult Deactivate(int id)
        {
            this.purchaseOrderService.Deactivate(id);

            return RedirectToAction("Index", "PurchaseOrder");
        }
    }
}