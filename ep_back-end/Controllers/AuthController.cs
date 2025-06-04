using ep_back_end.Entities;
using ep_back_end.Models;
using ep_back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ep_back_end.Controllers
{
    // Controllers/AuthController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel dto)
        {
            if (await _authRepo.UserExists(dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password, 
            };

            await _authRepo.Register(user, dto.Password);
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel dto)
        {
            var user = await _authRepo.Login(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var token = _authRepo.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }

}
