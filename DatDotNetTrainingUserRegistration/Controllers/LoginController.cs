using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DatDotNetTrainingUserRegistration.Dtos.LoginDto;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController()
        {
            _context = new AppDbContext();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Password == request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var newSession = new TblLogin
            {
                LoginId = Guid.NewGuid(),
                UserId = user.UserId,
                SessionId = Guid.NewGuid(),
                SessionTime = DateTime.UtcNow.AddMinutes(2)
            };

            _context.TblLogins.Add(newSession);
            await _context.SaveChangesAsync();

            var response = new LoginResponseDto
            {
                UserId = user.UserId,
                SessionId = newSession.SessionId,
            };

            return Ok(response);
        }
    }
}
