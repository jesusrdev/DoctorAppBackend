using Data;
using Data.Interfaces;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ExtensionApplicationService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        //* Configuring the swagger
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Enter Bearer [space] token \r\n\n " +
                              "Example: Bearer ejoy^888800009899999999",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        //* Connecting to the db by connection string
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        //* Fixing the cors problem
        services.AddCors();

        //* Adding the JWT service
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}