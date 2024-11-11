using System.Text;
using API.Extensions;
using Data;
using Data.Interfaces;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//* Adding the extension application service
builder.Services.AddApplicationServices(builder.Configuration);

//* Adding the extension identity service
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();