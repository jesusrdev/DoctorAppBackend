using API.Extensions;
using API.Middleware;
using Data.Initializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//* Adding the extension application service
builder.Services.AddApplicationServices(builder.Configuration);

//* Adding the extension identity service
builder.Services.AddIdentityServices(builder.Configuration);

//* Adding the initializer service
builder.Services.AddScoped<IDbInitializer, DbInitializer>();


var app = builder.Build();

//* The middleware was going before the pipeline
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");


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


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var initializer = services.GetRequiredService<IDbInitializer>();
        initializer.Initialize();
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An Error occurred while executing the migration");
    }   
}


app.MapControllers();

app.Run();