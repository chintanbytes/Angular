using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

class IdentityDb : IdentityDbContext<IdentityUser>
{
    public IdentityDb(DbContextOptions<IdentityDb> options) : base(options)
    {
    }
}