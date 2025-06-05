using BuyList.Data;
using BuyList.Models;
using Microsoft.EntityFrameworkCore;

namespace BuyList.Services;

public class ItemService
{
    private readonly BuyListContext _context;

    public ItemService(BuyListContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetOpenItemsAsync()
    {
        return await _context.Items
            .Where(i => !i.Done)
            .ToListAsync();
    }

    public async Task<Item?> GetItemAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item?> PatchItemDoneStateAsync(int id, bool done)
    {
        var item = await _context.Items.FindAsync(id);
        if(item == null)
            return null;
        
        item.Done = done;
        await _context.SaveChangesAsync();
        return item;
        
    }

    public async Task<Item> AddItemAsync(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return item;
        
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if(item == null)
            return false;
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}