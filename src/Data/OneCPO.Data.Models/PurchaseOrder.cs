using OneCPO.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace OneCPO.Data.Models
{
    public class PurchaseOrder : BaseModel<int>
    {
        public PurchaseOrder()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Status = StatusType.Active;
        }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public StatusType Status { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}