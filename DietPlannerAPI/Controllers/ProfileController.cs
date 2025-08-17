using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DietPlannerAPI.Models;
using DietPlannerAPI.DTOs;
using DietPlannerAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DietPlannerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 POST: Create new profile
        [HttpPost]
        public IActionResult SaveProfile([FromBody] ProfileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest("Validation Error: " + errors);
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token");

            if (_context.Profiles.Any(p => p.UserId == userId))
                return BadRequest("Profile already exists.");

            var profile = new Profile
            {
                Age = dto.Age,
                Gender = dto.Gender,
                Height = dto.Height,
                Weight = dto.Weight,
                FoodPreference = dto.FoodPreference,
                Goal = dto.Goal,
                UserId = userId
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return Ok(profile);
        }

        // 🔹 GET: Fetch your profile
        [HttpGet]
        public IActionResult GetProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token");

            var profile = _context.Profiles.FirstOrDefault(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found");

            return Ok(profile);
        }

        // 🔹 PUT: Update your profile
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] ProfileDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest("Validation Error: " + errors);
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token");

            var profile = _context.Profiles.FirstOrDefault(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found");

            // ✅ Update all fields
            profile.Age = dto.Age;
            profile.Gender = dto.Gender;
            profile.Height = dto.Height;
            profile.Weight = dto.Weight;
            profile.FoodPreference = dto.FoodPreference;
            profile.Goal = dto.Goal;

            // ✅ Track entity state
            _context.Entry(profile).State = EntityState.Modified;

            // 🔎 Debug output to verify update
            Console.WriteLine($"✅ Updated Profile for User {userId}: Age={dto.Age}, Gender={dto.Gender}, Height={dto.Height}, Weight={dto.Weight}, Goal={dto.Goal}");

            _context.SaveChanges();
            return Ok(profile);
        }

        // 🔹 DELETE: Delete your profile
        [HttpDelete]
        public IActionResult DeleteProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token");

            var profile = _context.Profiles.FirstOrDefault(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found");

            _context.Profiles.Remove(profile);
            _context.SaveChanges();

            return Ok("Profile deleted successfully.");
        }
    }
}
