using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Angular.DBContext;
public class ApplicationUser : IdentityUser
{
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [Column(TypeName = "datetimeoffset")]
    public DateTimeOffset? DateOfBirth { get; set; }
}