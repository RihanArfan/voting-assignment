namespace Web.Models;

public class ReportViewModel
{
    public Election Election { get; set; } = new();
    public IEnumerable<TimeSeriesVote> TimeSeriesVotes { get; set; } = default!;
    public IEnumerable<PartyVote> PartyVotes { get; set; } = default!;
    public int TotalVotes { get; set; }
}