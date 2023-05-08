using System.ComponentModel.DataAnnotations;

namespace Angular.Models;
public class UserDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    public DateTimeOffset DateOfBirth { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

    public UserDto()
    {
    }
}