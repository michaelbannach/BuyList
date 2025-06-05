using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BuyList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var user = User.Identity?.Name ?? "Unbekannt";

            return Ok(new { displayName = user });
        }
    }
}