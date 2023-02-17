namespace Services;

public class VoteService : IVoteService
{
    private readonly VotingContext _context;

    public VoteService(VotingContext context)
    {
        _context = context;
    }

    public async Task VoteAsync(Vote vote)
    {
        _context.Vote.Add(vote);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUserVotedAsync(int userId, int electionId)
    {
        return await _context.Vote.AnyAsync(v => v.UserId == userId && v.ElectionId == electionId);
    }
}