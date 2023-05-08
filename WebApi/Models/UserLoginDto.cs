using System.ComponentModel.DataAnnotations;

namespace Angular.Models;
public class UserLoginDto : BaseDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}