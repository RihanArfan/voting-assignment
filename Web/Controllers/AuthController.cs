using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        if (!isValid)
        {
            ModelState.AddModelError("Token", "The token provided is invalid.");
            return View(viewModel);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, token.UserId.ToString()),
            new Claim("ElectionId", token.ElectionId.ToString()),
            new Claim("TokenId", token.Id.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Vote");
    }
}