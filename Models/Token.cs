namespace Models;

public class Token : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int ElectionId { get; set; }
    public Election Election { get; set; }
    public string Value { get; set; }
    public bool IsOnline { get; set; }
}