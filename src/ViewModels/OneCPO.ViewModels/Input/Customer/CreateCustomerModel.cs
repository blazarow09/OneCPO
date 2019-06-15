using OneCPO.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OneCPO.ViewModels.Input.Customer
{
    public class CreateCustomerModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 10)]
        public string Telephone { get; set; }
    }
}