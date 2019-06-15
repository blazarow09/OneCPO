using System.ComponentModel.DataAnnotations;

namespace OneCPO.ViewModels.Input.PurchaseOrder
{
    public class CreatePurchaseOrderModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public int Customer { get; set; }
    }
}