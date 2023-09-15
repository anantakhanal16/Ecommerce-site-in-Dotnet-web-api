using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Get a reference to the application's service scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Get the database context
        var dbContext = services.GetRequiredService<StoreContext>();

        // Apply pending migrations and create the database if it doesn't exist
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Handle any exceptions, log them, etc.
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations or creating the database.");
    }
}

app.Run();
