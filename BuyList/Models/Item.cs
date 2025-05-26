namespace BuyList.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public bool Done { get; set; }
}