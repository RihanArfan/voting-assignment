using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace Web;

public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ITokenService _tokenService;

    public TokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor, ITokenService tokenService) :
        base(options, logger, encoder, clock)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionTokenValue = _httpContextAccessor.HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(sessionTokenValue))
        {
            return AuthenticateResult.Fail("Missing token");
        }

        var token = await _tokenService.GetByValue(sessionTokenValue);
        var isValid = _tokenService.Validate(token);

        if (isValid == null)
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, token.UserId.ToString())
        }, "token");

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        
        return AuthenticateResult.Success(ticket);
    }
}