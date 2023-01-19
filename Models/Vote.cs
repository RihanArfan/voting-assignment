namespace Models;

public class Vote : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int ElectionId { get; set; }
    public Election Election { get; set; }
}