using OneCPO.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OneCPO.ViewModels.Input.Customers
{
    public class CreateCustomerModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        public string Telephone { get; set; }
    }
}