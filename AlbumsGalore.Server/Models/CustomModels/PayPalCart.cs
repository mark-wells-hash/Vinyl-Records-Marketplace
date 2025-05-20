namespace AlbumsGalore.Server.Models.CustomModels
{
    public class PayPalCart
    {
        public PurchaseOrder[]? purchaseOrder { get; set; }
    }

    public class PurchaseOrder
    {
        public int referenceId { get; set; }
        public int customId { get; set; }
        public Amount? amount { get; set; }
        public Payee? payee { get; set; }
        public Items[]? items { get; set; }
    }

    public class Amount
    {
        public float totalCost { get; set; }
        public string? currency { get; set; }
        public float tax { get; set; }
        public int shipping { get; set; }
    }

    public class Payee
    {
        public string? merchantId { get; set; }
        public string? merchantEmail { get; set; }
    }

    public class Items
    {
        public string? itemName { get; set; }
        public string? itemDescription { get; set; }
        public int itemCost { get; set; }
    }

}
