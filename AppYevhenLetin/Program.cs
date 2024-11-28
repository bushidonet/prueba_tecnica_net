using Microsoft.EntityFrameworkCore;
using PruebaYevhenLetin.Controllers;
using PruebaYevhenLetin.Services;
using WebApplication1.Data;

namespace WebApplication1;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Database in memory
        builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseInMemoryDatabase("InMemoryDb"));
        
        //Services
        builder.Services.AddHttpClient<IApiService, ApiService>();
        builder.Services.AddScoped<IRetailer, RetailerService>();
        builder.Services.AddScoped<DbSeeder>();
        builder.Services.AddControllers();
        
        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        //Create DB
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
            await seeder.SeedDatabaseAsync();
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); //JSON
                options.DocumentTitle = "Adevinta"; // Title
                options.DisplayRequestDuration(); // time request
                
            });
        }
        
        //Middleware
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}