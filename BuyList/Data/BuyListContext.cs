using Microsoft.EntityFrameworkCore;
using BuyList.Models;
namespace BuyList.Data;

public class BuyListContext  : DbContext
{
    public BuyListContext(DbContextOptions<BuyListContext> options) : base(options) {}
    
    public DbSet<Item> Items { get; set; }
}