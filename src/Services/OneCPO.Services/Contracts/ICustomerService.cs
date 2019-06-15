﻿using OneCPO.Data.Models;
using OneCPO.ViewModels.Input.Customers;
using System.Linq;

namespace OneCPO.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<Customer> Sort(string sortOrder, IQueryable<Customer> customers);

        IQueryable<Customer> GetAll();

        int Create(CreateCustomerModel model);

        void EditCustomer(Customer input);

        void DeleteUser(int id);

        void Activate(int id);

        void Deactivate(int id);
    }
}