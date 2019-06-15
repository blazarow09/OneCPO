namespace OneCPO.ViewModels.Input.PurchaseOrder
{
    public class CreatePurchaseOrderModel
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Customer { get; set; }
    }
}