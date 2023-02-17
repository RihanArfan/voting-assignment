using Microsoft.AspNetCore.Authorization;
using Web.Models;

namespace Web.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var token = await _tokenService.GetByValue(viewModel.Token);
        var isValid = _tokenService.Validate(token);

        if (!isValid) return View(viewModel);
        
        HttpContext.Session.SetString("Token", viewModel.Token);

        return RedirectToAction("Index", "Vote");
    }
}