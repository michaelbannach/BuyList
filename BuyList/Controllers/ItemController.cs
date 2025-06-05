using BuyList.Services;
using BuyList.Models;
using Microsoft.AspNetCore.Mvc;
using BuyList.Dtos;

using Microsoft.AspNetCore.Authorization;


namespace BuyList.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemController(ItemService itemService)
    {

        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        
        return  await _itemService.GetOpenItemsAsync();
        
    }
        
       

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var item = await _itemService.GetItemAsync(id);
        if (item == null) return NotFound();
        return item;
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchItem(int id, [FromBody] ItemPatchDto update)
    {
        var updated = await _itemService.PatchItemDoneStateAsync(id, update.Done);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        var created = await _itemService.AddItemAsync(item);
        return CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
       
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
       var success = await _itemService.DeleteItemAsync(id);
       return success ? NoContent() : NotFound();
    }
}
