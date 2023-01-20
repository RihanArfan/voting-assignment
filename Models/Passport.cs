using System.ComponentModel.DataAnnotations;

namespace Models;

public class Passport : BaseModel
{
    public int UserId { get; set; }
    public User User { get; set; }
    [Required] public string Number { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
}