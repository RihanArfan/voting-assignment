namespace Models;

public class Vote : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int PartyId { get; set; }
    public Party Party { get; set; } = default!;

    public int TokenId { get; set; }
    public Token Token { get; set; } = default!;
}