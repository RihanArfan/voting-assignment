namespace Models;

public class Token : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int ElectionId { get; set; }
    public Election Election { get; set; } = default!;

    public string Value { get; set; } = default!;
    public bool IsOnlineVote { get; init; }

    public Vote? Vote { get; set; }
}