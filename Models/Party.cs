using System.ComponentModel.DataAnnotations;

namespace Models;

public class Party : BaseModel
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
    public string Logo { get; set; }
    public string Color { get; set; }
    
    public string? Website { get; set; }
    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? Instagram { get; set; }

    public List<Election> Election { get; set; }
}