using ApiFinal.Extensions;
using ApiFinal.Helpers;
using ApiFinal.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
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
    }
    catch (Exception ex)
    {
        // Handle any exceptions, log them, etc.
       
        logger.LogError(ex, "An error occurred while applying migrations or creating the database.");
    }
}

app.Run();
