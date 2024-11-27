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

        return services;
    }
}