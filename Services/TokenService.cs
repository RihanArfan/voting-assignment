namespace Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly VotingContext _context;
    private readonly ILogger<ITokenService> _logger;

    public TokenService(VotingContext context, ILogger<ITokenService> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    public Token Create(User user, Election election)
    {
        var token = new Token
        {
            UserId = user.Id,
            ElectionId = election.Id,
            Value = Guid.NewGuid().ToString(),
            IsOnline = user.IsOnlineVoter
        };

        _context.Token.Add(token);

        Notify(token);

        return token;
    }

    public async Task<Token?> GetByValue(string tokenValue)
    {
        return await _context.Token
            .Include(t => t.Election)
            .FirstOrDefaultAsync(t => t.Value == tokenValue);
    }

    public bool Validate(Token? token)
    {
        if (token == null) return false;
        if (token.Election.EndDate < DateTime.Now) return false;
        if (token.Vote != null) return false;

        // if application is running in local booth mode, prevent online tokens from being used and vice versa
        if (Convert.ToBoolean(_configuration["PhysicalBooth"]) != token.IsOnline)
            return false;

        return true;
    }

    public void DeleteExpired()
    {
        var tokens = _context.Token
            .Include(t => t.Election)
            .Where(t => t.Election.EndDate < DateTime.Now)
            .ToList();

        _context.Token.RemoveRange(tokens);
    }

    private void Notify(Token token)
    {
        // NOT IMPLEMENTED - Not possible for proof-of-concept assignment
        try
        {
            if (token.IsOnline)
            {
                // Call API to email using 3rd party email delivery service
            }
            // Call API to send postal mail 
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to notify user {UserId} of token {TokenId}", token.UserId, token.Id);
            throw;
        }
    }
}