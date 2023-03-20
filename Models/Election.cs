using System.ComponentModel.DataAnnotations;

namespace Models;

public class Election : BaseModel
{
    [Required] public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [DataType(DataType.DateTime)] public DateTimeOffset StartDate { get; set; }
    [DataType(DataType.DateTime)] public DateTimeOffset EndDate { get; set; }

    public List<Party> Parties { get; set; } = default!;
    public List<Token> Tokens { get; set; } = default!;
}