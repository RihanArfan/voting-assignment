using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class VoteViewModel
{
    [Required] public string Party { get; set; }
}