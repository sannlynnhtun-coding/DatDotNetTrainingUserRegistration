using DatDotNetTrainingUserRegistration.Domain.Features.Login;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DatDotNetTrainingUserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _loginService.Login(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
