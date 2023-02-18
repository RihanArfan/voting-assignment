using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Authorize]
public class VoteController : Controller
{
    private readonly IElectionService _electionService;
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService, IElectionService electionService)
    {
        _voteService = voteService;
        _electionService = electionService;
    }

    public async Task<IActionResult> Index()
    {
        var electionId = HttpContext.User.FindFirst("ElectionId").Value;
        var election = await _electionService.GetAsync(int.Parse(electionId));
        return View(election);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Vote vote)
    {
        // check if HttpContext identity claim "ElectionId" with tokenService.GetByValue() and Validate()
        // if it's invalid, sign user out
        var sessionToken = HttpContext.User.FindFirst("TokenId").Value;

        // _voteService.Create(vote, sessionToken);

        return RedirectToAction("Confirmation");
    }

    public async Task<IActionResult> Confirmation()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }
}