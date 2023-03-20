using System.ComponentModel.DataAnnotations;

namespace Models;

public class User : BaseModel
{
    [Required] public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address1 { get; set; } = default!;
    public string? Address2 { get; set; }
    public string City { get; set; } = default!;
    public string County { get; set; } = default!;
    public string PostCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    public bool IsOnlineVoter { get; set; }

    public List<Vote> Votes { get; set; } = default!;
    public List<Token> Tokens { get; set; } = default!;

    public NationalInsurance? NationalInsurance { get; set; }
    public List<Passport> Passport { get; set; } = default!;
}