using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DatDotNetTrainingUserRegistration.Dtos.LoginDto;
using static DatDotNetTrainingUserRegistration.Dtos.ProfileDto;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserSessionService _userSessionService;

        public ProfileController()
        {
            _context = new AppDbContext();
            _userSessionService = new UserSessionService();
        }

        [HttpPost]
        public async Task<IActionResult> ProfileData([FromBody] ProfileRequestDto request)
        {
            if (!_userSessionService.IsSessionValid(request.UserId, request.SessionId))
                return Unauthorized("Invalid or expired session.");

            var userProfile = await _context.TblUsers
                .Select(u => new ProfileResponseDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName
                })
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (userProfile == null)
                return NotFound("User profile not found.");

            return Ok(userProfile);
        }
    }
}
