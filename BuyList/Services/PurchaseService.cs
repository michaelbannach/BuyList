using BuyList.Data;
using BuyList.Dtos;
using BuyList.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyList.Services;

public class PurchaseService
{
    private readonly BuyListContext _context;

    public PurchaseService(BuyListContext context)
    {
        _context = context;
    }

    public async Task<Purchase?> CreatePurchaseAsync(string userId, CreatePurchaseDto dto)
    {
        var items = await _context.Items
            .Where(i => dto.ItemIds.Contains(i.Id))
            .ToListAsync();

        if (!items.Any()) return null;

        foreach (var item in items)
            item.Done = true;

        var purchase = new Purchase
        {
            UserId = userId,
            PurchaseDate = DateTime.UtcNow,
            Price = dto.Price,
            Items = items.Select(i => new PurchasedItem { ItemId = i.Id }).ToList()
        };

        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        return purchase;
    }

    public async Task<List<PurchaseDto>> GetPurchasesForUserAsync(string userId)
    {
        var purchases = await _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Items)
            .ThenInclude(pi => pi.Item)
            .ToListAsync();

        return purchases.Select(p => new PurchaseDto
        {
            Id = p.Id,
            UserId = p.UserId,
            PurchaseDate = p.PurchaseDate,
            Price = p.Price,
            Items = p.Items.Select(pi => new PurchasedItemDto
            {
                Id = pi.Id,
                ItemId = pi.ItemId,
                Name = pi.Item?.Name ?? "",
                Amount = pi.Item?.Amount ?? ""
            }).ToList()
        }).ToList();
    }
}