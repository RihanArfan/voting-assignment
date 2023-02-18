using System.ComponentModel.DataAnnotations;

namespace Models;

public class Passport : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    [Required] public string Number { get; set; }
    public DateTimeOffset IssuedAt { get; set; }
    public DateTimeOffset ExpiredAt { get; set; }
}