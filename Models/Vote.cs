namespace Models;

public class Vote : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int ElectionId { get; set; }
    public Election Election { get; set; }

    public int PartyId { get; set; }
    public Party Party { get; set; }

    public int TokenId { get; set; }
    public Token Token { get; set; }
}