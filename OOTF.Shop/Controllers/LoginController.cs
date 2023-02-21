using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OOTF.Shopping.Context;
using OOTF.Shopping.Interfaces;
using OOTF.Shopping.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using OOTF.Shopping.Dto;

namespace OOTF.Shopping.Controllers
{
    public class LoginController : Controller 
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsernameAndPassword(string username, string email, string password)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(u => u.Name == username && u.Password == password);

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDto.Login login)
        {
            var user = await _userService.GetUserByUsernameAndPassword(login.Username,  login.Password);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var token = GenerateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetUserByUsername(model.Name);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = UserRole.Salesman// or whichever default role you want to assign to new users
            };

            await _userService.CreateUser(user);

            return Ok();
        }

        private string GenerateToken(User user)
        { 
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Password),
            new Claim(ClaimTypes.Email, user.Email)
            // Add any additional claims here
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
