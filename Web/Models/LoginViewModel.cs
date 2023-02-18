using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Token is required.")]
    [DisplayName("Unique Voter Token")]
    [DataType(DataType.Password)]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "Token must be 13 characters long.")]
    [RegularExpression(@"^[a-zA-Z0-9]{6}-[a-zA-Z0-9]{6}$", ErrorMessage = "Token must be in the format XXXXXX-XXXXXX.")]
    public string Token { get; set; }
}