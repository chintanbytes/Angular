using System.ComponentModel.DataAnnotations;

namespace MyShop.WebApi.Dtos;
public class UserLoginDto : BaseDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}