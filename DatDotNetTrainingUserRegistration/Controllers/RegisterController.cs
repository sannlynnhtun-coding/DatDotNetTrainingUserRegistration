using Microsoft.AspNetCore.Mvc;
using DatDotNetTrainingUserRegistration.Domain.Features.Register;
using DatDotNetTrainingUserRegistration.Dtos;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto request)
        {
           var result = await _registerService.Register(request);
            if (result.IsValidationError)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
