using OneCPO.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneCPO.Data.Models
{
    public class Customer : BaseModel<int>
    {
        public Customer()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.CreatedOn = DateTime.UtcNow;
            this.Status = StatusType.Active;
        }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        public string Telephone { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public StatusType Status { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}