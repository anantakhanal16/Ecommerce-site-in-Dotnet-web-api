using ApiFinal.Extensions;
using ApiFinal.Helpers;
using ApiFinal.Middlewares;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddApplicationServices();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddDbContext<AppIdentityDbContext>(x =>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();
app.MapControllers();

// Get a reference to the application's service scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        // Get the database context
        var context = services.GetRequiredService<StoreContext>();

        // Apply pending migrations and create the database if it doesn't exist
        context.Database.Migrate();
        //dataseeding 
        await StoreContextSeed.SeedAsync(context, loggerFactory);

        //configuring identity and migrating 
        var userManger = services.GetRequiredService<UserManager<AppUser>>();

        var identityContext = services.GetRequiredService<AppIdentityDbContext>();

        await identityContext.Database.MigrateAsync();

        await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
    }
    catch (Exception ex)
    {

       
        logger.LogError(ex, "An error occurred while applying migrations or creating the database.");
    }
}

app.Run();
