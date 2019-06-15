using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneCPO.Data.Models;
using OneCPO.Services.Contracts;
using OneCPO.ViewModels.Input.Customer;
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

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerModel customer)
        {
            this.customerService.Create(customer);

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            this.customerService.DeleteUser(id);

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult Activate(int id)
        {
            this.customerService.Activate(id);

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult Deactivate(int id)
        {
            this.customerService.Deactivate(id);

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = this.customerService.GetSingleCustomer(id);

            return this.View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer input)
        {
            this.customerService.EditCustomer(input);

            return RedirectToAction("Info", "Customer", input);
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            var customer = this.customerService.GetSingleCustomer(id);

            return this.View(customer);
        }
    }
}