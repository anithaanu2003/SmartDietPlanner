using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DietPlannerAPI.Services;
using DietPlannerAPI.Data;
using DietPlannerAPI.DTOs;
using System.Security.Claims;

namespace DietPlannerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MealPredictionService _predictor;

        public PredictionController(ApplicationDbContext context, MealPredictionService predictor)
        {
            _context = context;
            _predictor = predictor;
        }

        [HttpGet]
        public IActionResult GetMealPlan()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token");

            var profile = _context.Profiles.FirstOrDefault(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            // ✅ STEP 1: Debug log for Age, Height, Weight
            Console.WriteLine("📊 PROFILE DATA >>> Age: " + profile.Age + ", Height: " + profile.Height + ", Weight: " + profile.Weight);

            var meals = _predictor.GenerateMealPlan(profile);
            var calories = _predictor.CalculateTDEE(profile);

            return Ok(new PredictionResponseDTO
            {
                CalorieGoal = calories,
                Meals = meals
            });
        }
    }
}
