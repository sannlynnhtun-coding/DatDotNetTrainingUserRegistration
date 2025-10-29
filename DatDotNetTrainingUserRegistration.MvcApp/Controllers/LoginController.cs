using DatDotNetTrainingUserRegistration.Domain.Features.Login;
using DatDotNetTrainingUserRegistration.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        var jsonStr = JsonConvert.SerializeObject(result);
        HttpContext.Session.SetString("Login", jsonStr);

        return Redirect("/Home");
    }
}