using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class UserApplication : IdentityUser<int>
{
    public string Lastname { get; set; }

    public string Firstname { get; set; }
    
    public ICollection<UserRoleApplication> UserRoles { get; set; }
}