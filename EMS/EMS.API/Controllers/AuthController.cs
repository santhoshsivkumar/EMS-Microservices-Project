using EMS.Infrastructure.Security;
using EMS.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using EMS.Application.DTOs;
using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly EMSDbContext _context;
        public readonly JwtService _jwtService;

        public AuthController(EMSDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);   
            if(user != null)
            {
                return BadRequest("Username already exists");
            }

            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();  

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>u.Username == dto.Username);

            if(user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid Credentials");

            var token = _jwtService.GenerateToken(user);


            return Ok(new
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            });
        }


    }
}
