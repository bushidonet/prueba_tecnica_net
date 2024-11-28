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

        builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseInMemoryDatabase("InMemoryDb"));
        builder.Services.AddHttpClient<IApiService, ApiService>();
        builder.Services.AddScoped<IRetailer, RetailerService>();
        builder.Services.AddScoped<DbSeeder>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
            await seeder.SeedDatabaseAsync();
        }
        
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}