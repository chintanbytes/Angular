using System.ComponentModel.DataAnnotations;

public class NewUserInfo
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

    public NewUserInfo(string username, string password, string confirmPassword)
    {
        Username = username;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public NewUserInfo()
    {
    }

    public override string ToString()
    {
        return $"{Username} {Password}";
    }
}