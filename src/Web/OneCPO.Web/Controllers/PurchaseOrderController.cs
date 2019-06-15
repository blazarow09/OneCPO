using Microsoft.AspNetCore.Mvc;
using OneCPO.Services.Contracts;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            var purchases = this.purchaseOrderService.GetAllPurchases();

            return this.View(purchases);
        }
    }
}
