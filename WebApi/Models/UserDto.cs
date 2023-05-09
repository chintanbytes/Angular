using System.ComponentModel.DataAnnotations;

namespace MyShop.WebApi.Models;
public class UserDto : BaseDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}