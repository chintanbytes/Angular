using Microsoft.AspNetCore.Identity;

namespace MyShop.WebApi.Data;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int Age => CalculateAge();

    private int CalculateAge()
    {
        DateTime currentDate = DateTime.UtcNow;
        int age = currentDate.Year - DateOfBirth.Year;

        // Adjust age if the birthdate hasn't occurred yet this year
        if (currentDate < DateOfBirth.AddYears(age))
        {
            age--;
        }

        return age;
    }
}