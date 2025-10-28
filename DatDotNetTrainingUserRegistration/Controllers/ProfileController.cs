using DatDotNetTrainingUserRegistration.Database.AppDbContextModels;
using DatDotNetTrainingUserRegistration.Domain.Features.Login;
using DatDotNetTrainingUserRegistration.Domain.Features.Profile;
using DatDotNetTrainingUserRegistration.Domain.Services;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> GetProfile([FromBody] ProfileRequestDto request)
        {
            var result = await _profileService.GetProfile(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
