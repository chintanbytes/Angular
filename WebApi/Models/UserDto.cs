using System.ComponentModel.DataAnnotations;
using MyShop.WebApi.CustomValidationAttributes;

namespace MyShop.WebApi.Models;

[UserDtoValidationAttribute]
public class UserDto : BaseDto, IValidatableObject
{
    [Required]
    [MaxLength(256)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string ConfirmPassword { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Password != ConfirmPassword)
            yield return new ValidationResult("Passwords do not match", new[] { nameof(Password), nameof(ConfirmPassword) });
    }
}