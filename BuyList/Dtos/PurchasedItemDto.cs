namespace BuyList.Dtos;

public class PurchasedItemDto
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Amount { get; set; }
}