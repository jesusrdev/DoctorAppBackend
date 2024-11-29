using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<UserApplication> _userManager;
    private readonly RoleManager<RoleApplication> _roleManager;

    public DbInitializer(ApplicationDbContext db, UserManager<UserApplication> userManager,
        RoleManager<RoleApplication> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void Initialize()
    {
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();  // Only the first time of our app it would make the migrations
            }
        }
        catch (Exception e)
        {
            throw;
        }

        if (_db.Roles.Any(r => r.Name == "Admin")) return;

        _roleManager.CreateAsync(new RoleApplication() { Name = "Admin" }).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new RoleApplication() { Name = "Scheduler" }).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new RoleApplication() { Name = "Doctor" }).GetAwaiter().GetResult();

        //* Create admin user
        var user = new UserApplication()
        {
            UserName = "admin",
            Email = "admin@doctorapp.com",
            Lastname = "Petro",
            Firstname = "Carlos",
        };

        _userManager.CreateAsync(user, "Admin123").GetAwaiter().GetResult();
        
        //* Assign the role admin to the user
        UserApplication userAdmin = _db.UserApplication.Where(u => u.UserName == "admin").FirstOrDefault();
        _userManager.AddToRoleAsync(userAdmin, "Admin").GetAwaiter().GetResult();
    }
}