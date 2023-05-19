using System.ComponentModel.DataAnnotations;
using MyShop.WebApi.Dtos;

namespace MyShop.WebApi.ValidationAttributes;

public class UserDtoValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value != null  && value is UserDto dto)
        {
            return dto.Password == dto.ConfirmPassword && !string.IsNullOrEmpty(dto.Username);
        }

        return false;
    }
}