using System.ComponentModel.DataAnnotations;
using MyShop.WebApi.Models;

namespace MyShop.WebApi.ValidationAttributes;

public class UserDtoValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var userDto = value as UserDto;
        if (userDto == null || userDto.GetType() != typeof(UserDto))
        {
            return false;
        }

        return userDto.Password != userDto.ConfirmPassword && !string.IsNullOrEmpty(userDto.Username);
    }
}


