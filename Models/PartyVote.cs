namespace Models;

public class PartyVote
{
    public int PartyId { get; set; }
    public string PartyName { get; set; } = default!;
    public string PartyColor { get; set; } = default!;
    public int Total { get; set; }
}