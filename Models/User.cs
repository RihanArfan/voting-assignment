using System.ComponentModel.DataAnnotations;

namespace Models;

public class User : BaseModel
{
    [Required] public string Name { get; set; }
    public string Email { get; set; }
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    public bool IsOnlineVoter { get; set; }
    
    public List<Vote> Votes { get; set; }
    public List<Token> Tokens { get; set; }

}