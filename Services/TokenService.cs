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

    /// <summary>
    /// Issue a new token for a user to vote on an election
    /// </summary>
    /// <param name="user"></param>
    /// <param name="election"></param>
    /// <returns></returns>
    public Token Create(User user, Election election)
    {
        var token = new Token
        {
            UserId = user.Id,
            ElectionId = election.Id,
            Value = Guid.NewGuid().ToString(),
            IsOnlineVote = user.IsOnlineVoter
        };

        _context.Token.Add(token);

        Notify(token);

        return token;
    }

    /// <summary>
    /// Get token by its value
    /// </summary>
    /// <param name="tokenValue"></param>
    /// <returns></returns>
    public async Task<Token?> GetByValue(string tokenValue)
    {
        return await _context.Token
            .Include(t => t.Election)
            .Include(t => t.Vote)
            .FirstOrDefaultAsync(t => t.Value == tokenValue);
    }

    /// <summary>
    /// Validate a token is valid for voting
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public bool Validate(Token? token)
    {
        if (token == null) return false;
        if (token.Election.EndDate < DateTime.Now) return false;
        if (token.Vote != null) return false;

        // if application is running in local booth mode, prevent online tokens from being used and vice versa
        if (Convert.ToBoolean(_configuration["PhysicalBooth"]) != token.IsOnlineVote)
            return false;

        return true;
    }

    /// <summary>
    /// Notify user of their token
    /// </summary>
    /// <param name="token"></param>
    private void Notify(Token token)
    {
        // NOT IMPLEMENTED - Not possible for proof-of-concept assignment
        try
        {
            if (token.IsOnlineVote)
            {
                // Call API to send email
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