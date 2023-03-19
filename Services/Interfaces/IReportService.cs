namespace Services.Interfaces;

public interface IReportService
{
    Task<int> GetVoteCount(int electionId);

    IEnumerable<TimeSeriesVote> GetTimeSeriesVotes(int electionId);
    IEnumerable<PartyVote> GetPartyVotes(int electionId);
}