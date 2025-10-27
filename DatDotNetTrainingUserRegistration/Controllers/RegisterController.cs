using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using static DatDotNetTrainingUserRegistration.Dtos.RegisterDto;
using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegisterController()
        {
            _context = new AppDbContext();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto request)
        {
            if (_context.TblUsers.Any(u => u.UserName == request.UserName))
                return BadRequest("Username already exists.");


            var newUser = new TblUser
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                FullName = request.FullName,
                Password = request.Password
            };

            _context.TblUsers.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
    }
}
