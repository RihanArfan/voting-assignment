using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class VoteController : Controller
{
    private readonly IElectionService _electionService;
    private readonly ITokenService _tokenService;
    private readonly IVoteService _voteService;


    public VoteController(IVoteService voteService, IElectionService electionService, ITokenService tokenService)
    {
        _voteService = voteService;
        _electionService = electionService;
        _tokenService = tokenService;
    }

    public async Task<IActionResult> Index()
    {
        var electionId = HttpContext.User.FindFirst("ElectionId")?.Value;
        var election = await _electionService.GetAsync(int.Parse(electionId ?? throw new InvalidOperationException()));
        return View(election);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(VoteViewModel viewModel)
    {
        // handle invalid model state
        if (!ModelState.IsValid)
        {
            var electionId = HttpContext.User.FindFirst("ElectionId")?.Value;
            var election =
                await _electionService.GetAsync(int.Parse(electionId ?? throw new InvalidOperationException()));
            return View(election);
        }

        try
        {
            // get user token
            var sessionToken = HttpContext.User.FindFirst("Token")?.Value;
            await _voteService.VoteAsync(viewModel.Party, sessionToken ?? throw new InvalidOperationException());
        }
        catch (UnauthorizedAccessException)
        {
            // handle session errors
            ModelState.AddModelError("", "Session expired, sign in again.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        catch (InvalidOperationException)
        {
            // handle session errors
            ModelState.AddModelError("", "Session expired, sign in again.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return RedirectToAction("Confirmation");
    }


    public async Task<IActionResult> Confirmation()
    {
        // show what vote number the user is
        var VOTE_NUMBER = 12940; // TODO: get from VoteService.VoteAsync
        var voteNumberNearest100 = (VOTE_NUMBER + 50) / 100 * 100;
        ViewBag.VoteNumber = voteNumberNearest100.ToString("N0");

        // logout the user as they have voted now
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }
}