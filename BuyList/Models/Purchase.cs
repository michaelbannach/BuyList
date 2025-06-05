namespace BuyList.Models;

public class Purchase
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    
    public decimal Price { get; set; }
    public List<PurchasedItem> Items { get; set; } = new();
}

