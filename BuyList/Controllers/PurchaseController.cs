using Microsoft.AspNetCore.Mvc;
using BuyList.Services;
using BuyList.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace BuyList.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> Purchase([FromBody] CreatePurchaseDto dto)
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var result = await _purchaseService.CreatePurchaseAsync(userId, dto);
        if (result == null)
            return BadRequest("Keine gültigen Items gefunden.");

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var purchases = await _purchaseService.GetPurchasesForUserAsync(userId);
        return Ok(purchases);
    }
}