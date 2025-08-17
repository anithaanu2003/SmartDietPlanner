using Microsoft.AspNetCore.Mvc;
using DietPlannerAPI.DTOs;
using DietPlannerAPI.Services;
using DietPlannerAPI.Data;
using DietPlannerAPI.Models;
using System.Linq;

namespace DietPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _auth;
        private readonly JwtService _jwt;

        public AuthController(ApplicationDbContext context, AuthService auth, JwtService jwt)
        {
            _context = context;
            _auth = auth;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("User already exists.");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = _auth.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { user.Id, user.Email });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || !_auth.VerifyPassword(user.PasswordHash, dto.Password))
                return Unauthorized("Invalid credentials.");

            var token = _jwt.GenerateToken(user.Id, user.Email);

            return Ok(new { token });
        }
    }
}
