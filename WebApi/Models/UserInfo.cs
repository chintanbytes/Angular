using System.ComponentModel.DataAnnotations;

public class UserInfo
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public UserInfo(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public UserInfo()
    {
    }

    public override string ToString()
    {
        return $"{Username} {Password}";
    }

}