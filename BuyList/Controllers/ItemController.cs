using BuyList.Data;
using BuyList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuyList.Dtos;
namespace BuyList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly BuyListContext _context;

    public ItemController(BuyListContext context)
    {

        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems() =>

        await _context.Items.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
            return NotFound();
        return item;

    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchItem(int id, [FromBody] ItemPatchDto update)
    {
        var item = await _context.Items.FindAsync(id);
        if(item == null)
            return NotFound();
       
        item.Done = update.Done;
        
        await _context.SaveChangesAsync();
        
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
        var item = await _context.Items.FindAsync();
        if(item == null)
            return NotFound();
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
