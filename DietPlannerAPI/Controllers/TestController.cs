using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize] // 🔐 Only authenticated users can access
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok("✅ This is a protected route. You are authenticated!");
        }

        [AllowAnonymous] // 🌐 Anyone can access
        [HttpGet("open")]
        public IActionResult OpenEndpoint()
        {
            return Ok("🌐 This is a public route. Anyone can access.");
        }
    }
}
