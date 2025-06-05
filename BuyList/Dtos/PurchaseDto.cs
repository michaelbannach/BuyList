namespace BuyList.Dtos;

public class PurchaseDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public decimal Price { get; set; }
    public List<PurchasedItemDto> Items { get; set; } = new(); 
}