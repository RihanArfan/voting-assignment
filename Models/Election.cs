using System.ComponentModel.DataAnnotations;

namespace Models;

public class Election : BaseModel
{
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public List<Party> Parties { get; set; }
    public List<Vote> Votes { get; set; }
}