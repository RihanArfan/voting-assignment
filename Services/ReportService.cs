namespace Services;

public class ReportService
{
    private readonly VotingContext _context;

    public ReportService(VotingContext context)
    {
        _context = context;
    }

    public int GetVotesCount(int electionId)
    {
        return _context.Vote.Count(v => v.ElectionId == electionId);
    }

    public IEnumerable<TimeSeriesVote> GetTimeSeriesVotesByElection(int electionId)
    {
        return _context.Vote
            .Where(v => v.ElectionId == electionId)
            .GroupBy(v => v.CreatedAt.Date)
            .Select(g => new TimeSeriesVote
            {
                Date = g.Key,
                Count = g.Count()
            });
    }
}