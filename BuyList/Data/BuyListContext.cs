using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuyList.Models;

namespace BuyList.Data;

public class BuyListContext : DbContext
{
    public BuyListContext(DbContextOptions<BuyListContext> options) : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();

    public DbSet<Purchase> Purchases => Set<Purchase>();

    public DbSet<PurchasedItem> PurchasedItems => Set<PurchasedItem>();



}