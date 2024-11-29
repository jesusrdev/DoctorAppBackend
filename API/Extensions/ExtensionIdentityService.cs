using System.Text;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;

namespace API.Extensions;

public static class ExtensionIdentityService
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        //* Adding identity service
        services.AddIdentityCore<UserApplication>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<RoleApplication>()
        .AddRoleManager<RoleManager<RoleApplication>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
        
        //* Adding jwt bearer service
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });


        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
            opt.AddPolicy("AdminSchedulerRole", policy => policy.RequireRole("Admin", "Scheduler"));
            opt.AddPolicy("AdminDoctorRole", policy => policy.RequireRole("Admin", "Doctor"));
        });

        return services;
    }
}