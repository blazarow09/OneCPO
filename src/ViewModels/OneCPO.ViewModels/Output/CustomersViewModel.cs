using OneCPO.Data.Models.Enums;
using System;

namespace OneCPO.ViewModels.Output
{
    public class CustomersViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        public string Telephone { get; set; }

        public DateTime CreatedOn { get; set; }

        public StatusType Status { get; set; }
    }
}