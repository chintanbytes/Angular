using System.ComponentModel.DataAnnotations;
using MyShop.WebApi.Models;

namespace MyShop.WebApi.ValidationAttributes;

public class ProductDtoValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not ProductDto productDto)
        {
            throw new Exception("ProductDtoValidationAttribute can only be applied to ProductDto");
        }

        if (productDto.ProductName == null)
        {
            return new ValidationResult("ProductName cannot be null",
                new[] { nameof(productDto.ProductName) });
        }

        if (productDto.ProductName.Length > 40)
        {
            return new ValidationResult("ProductName cannot be longer than 40 characters",
            new[] { nameof(productDto.ProductName) });
        }

        if (productDto.UnitPrice < 0)
        {
            return new ValidationResult("UnitPrice cannot be less than 0",
            new[] { nameof(productDto.UnitPrice) });
        }

        return ValidationResult.Success;
    }
}


