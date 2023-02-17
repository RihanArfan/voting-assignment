using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class LoginViewModel : AlertModel
{
    [Required]
    public string Token { get; set; }
}