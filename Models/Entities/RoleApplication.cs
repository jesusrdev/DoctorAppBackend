using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class RoleApplication : IdentityRole<int>
{
    public ICollection<UserRoleApplication> UserRoles { get; set; }
}