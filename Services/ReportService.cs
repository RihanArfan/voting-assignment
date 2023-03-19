namespace Services;

// TODO: Interface
public class ReportService : IReportService
{
    private readonly VotingContext _context;

    public ReportService(VotingContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Get total vote count by election
    /// </summary>
    /// <param name="electionId"></param>
    /// <returns></returns>
    public async Task<int> GetVoteCount(int electionId)
    {
        return await _context.Vote.CountAsync(v => v.Token.ElectionId == electionId);
    }

    /// <summary>
    ///     Returns total, online and in person vote counts by date for an election
    /// </summary>
    /// <param name="electionId"></param>
    /// <returns></returns>
    public IEnumerable<TimeSeriesVote> GetTimeSeriesVotes(int electionId)
    {
        return _context.Vote
            .Where(v => v.Token.ElectionId == electionId)
            .GroupBy(v => v.CreatedAt)
            .Select(g => new TimeSeriesVote
            {
                Date = g.Key.UtcDateTime,
                Total = g.Count(),
                Online = g.Count(v => v.Token.IsOnlineVote),
                InPerson = g.Count(v => !v.Token.IsOnlineVote)
            })
            .ToList();
    }

    public IEnumerable<PartyVote> GetPartyVotes(int electionId)
    {
        return _context.Vote
            .Where(v => v.Token.ElectionId == electionId)
            .GroupBy(v => v.PartyId)
            .Select(g => new PartyVote
            {
                PartyId = g.Key,
                PartyName = g.First().Party.Name,
                PartyColor = g.First().Party.Color,
                Total = g.Count()
            })
            .ToList();
    }
}