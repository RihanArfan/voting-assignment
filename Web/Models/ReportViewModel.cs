namespace Web.Models;

public class ReportViewModel
{
    public Election Election { get; set; }
    public IEnumerable<TimeSeriesVote> TimeSeriesVotes { get; set; }
    public IEnumerable<PartyVote> PartyVotes { get; set; }
    public int TotalVotes { get; set; }
}