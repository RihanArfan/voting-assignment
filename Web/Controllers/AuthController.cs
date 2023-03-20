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
    private readonly IVoteService _voteService;

    public AuthController(ITokenService tokenService, IVoteService voteService)
    {
        _tokenService = tokenService;
        _voteService = voteService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        // handle invalid model state
        if (!ModelState.IsValid) return View(viewModel);

        // validate token
        var token = await _tokenService.GetByValue(viewModel.Token);
        var isValid = _tokenService.Validate(token);

        // handle invalid token
        if (!isValid || token == null)
        {
            ModelState.AddModelError("Token", "The token provided is invalid.");
            return View(viewModel);
        }

        // prepare details login
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, token.UserId.ToString()),
            new("ElectionId", token.ElectionId.ToString()),
            new("Token", token.Value)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // login user
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Vote");
    }
}