using System.ComponentModel.DataAnnotations;

namespace MyShop.WebApi.Models;
public class UserLoginDto : BaseDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}