using System.ComponentModel.DataAnnotations;

namespace MyShop.WebApi.Dtos;
public class CustomerDto : UserDto
{
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(30)]
    public string LastName { get; set; }

    [MaxLength(60)]
    public string? Address { get; set; }

    [MaxLength(15)]
    public string? City { get; set; }

    [MaxLength(15)]
    public string? State { get; set; }

    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [MaxLength(15)]
    public string? Country { get; set; }

    [MaxLength(24)]
    public string? Cell { get; set; }

    [MaxLength(24)]
    public string? Phone { get; set; }
}