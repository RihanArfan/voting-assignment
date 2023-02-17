namespace Services;

public class UserService
{
    private readonly VotingContext _context;
    private readonly ITokenService _tokenService;

    public UserService(VotingContext context, ILogger<UserService> logger, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public void CreateTokensForElectionAsync(Election election)
    {
        var users = _context.User.ToList();

        foreach (var user in users) _tokenService.Create(user, election);

        _context.SaveChanges();
    }
}