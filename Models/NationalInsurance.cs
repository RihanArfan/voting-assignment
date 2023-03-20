namespace Models;

public class NationalInsurance : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public string Number { get; set; } = default!;
}