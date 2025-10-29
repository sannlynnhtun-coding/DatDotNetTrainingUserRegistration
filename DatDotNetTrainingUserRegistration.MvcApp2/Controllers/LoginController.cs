using DatDotNetTrainingUserRegistration.Domain.Features.Login;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatDotNetTrainingUserRegistration.MvcApp.Controllers;

public class LoginController : Controller
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginRequestDto request)
    {
        var result = await _loginService.Login(request);
        if (!result.IsSuccess)
        {
            // ViewData
            // ViewBag
            // TempData

            TempData["Message"] = result.Message;
            return View();
        }

        var claims = new List<Claim>
            {
                new Claim("UserId", result.UserId.ToString()),
                new Claim("SessionId", result.SessionId.ToString()),
            };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //IsPersistent = model.RememberMe, // "Remember me" option
            ExpiresUtc = DateTime.Now.AddHours(1)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Redirect("/Home");
    }
}