using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class UserRoleApplication : IdentityUserRole<int>
{
    public UserApplication UserApplication { get; set; }
    
    public RoleApplication RoleApplication { get; set; }
}