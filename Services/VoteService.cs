namespace Services;

public class VoteService : IVoteService
{
    private readonly VotingContext _context;
    private readonly ITokenService _tokenService;
    private readonly IElectionService _electionService;

    public VoteService(VotingContext context, ITokenService tokenService, IElectionService electionService)
    {
        _context = context;
        _tokenService = tokenService;
        _electionService = electionService;
    }

    public async Task VoteAsync(string partyId, string tokenValue)
    {
        var token = await _tokenService.GetByValue(tokenValue);

        if (token == null) throw new UnauthorizedAccessException();

        var isUserVotedAsync = await IsUserVotedAsync(token.UserId, token.ElectionId);
        if (isUserVotedAsync) throw new InvalidOperationException();

        var election = await _electionService.GetAsync(token.ElectionId);

        var isPartyExist = election.Parties.Any(party => party.Id == int.Parse(partyId));
        if (!isPartyExist) throw new InvalidOperationException();

        var vote = new Vote
        {
            ElectionId = token.ElectionId,
            UserId = token.UserId,
            TokenId = token.Id,
            PartyId = int.Parse(partyId),
            CreatedAt = DateTimeOffset.Now
        };

        _context.Vote.Add(vote);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUserVotedAsync(int userId, int electionId)
    {
        return await _context.Vote.AnyAsync(v => v.UserId == userId && v.ElectionId == electionId);
    }
}