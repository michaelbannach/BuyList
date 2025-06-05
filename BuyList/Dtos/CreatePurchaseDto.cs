namespace BuyList.Dtos;

public class CreatePurchaseDto
{
    public List<int> ItemIds { get; set; } = new();
    public decimal Price { get; set; }
}