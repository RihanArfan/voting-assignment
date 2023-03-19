namespace Models;

public class TimeSeriesVote
{
    public DateTime Date { get; set; }
    public int Total { get; set; }
    public int Online { get; set; }
    public int InPerson { get; set; }
}