using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneCPO.Services.Contracts;
using ReflectionIT.Mvc.Paging;

namespace OneCPO.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, int page = 1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenderSortParm"] = sortOrder == "Gender" ? "gender_desc" : "Gender";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["DateSortParm"] = sortOrder == "Status" ? "Date_desc" : "Date";

            var customers = this.customerService.GetAll();

            customers = this.customerService.Sort(sortOrder, customers);

            var model = await PagingList.CreateAsync(customers.OrderBy(p => 0), 6, page);

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            return View(model);
        }
    }
}