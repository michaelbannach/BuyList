namespace BuyList.Models;

public class PurchasedItem

    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
