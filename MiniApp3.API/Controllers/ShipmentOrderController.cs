using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace MiniApp3.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentOrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userName = HttpContext.User.Identity.Name;
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return Ok($"ShipmentOrder - Username: {userName} - UserId: {userId}");
        }
    }
}
