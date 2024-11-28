using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace PruebaYevhenLetin.Services;

public class DbSeeder
{
    private readonly IApiService _apiService;
    private readonly AppDbContext _context;

    public DbSeeder(IApiService apiService, AppDbContext data)
    {
        _apiService = apiService;
        _context = data;
    }

    // Metod insert data to daatabase from API 
    public async Task SeedDatabaseAsync()
    {
        var retailersFromApi = await _apiService.GetAllRetailersFromApiAsync();
    
        foreach (var retailer in retailersFromApi)
        {
            var existingRetailer = await _context.Retailers
                .FirstOrDefaultAsync(r => r.ReCode == retailer.ReCode);
        
            if (existingRetailer == null)
            {
                _context.Retailers.Add(retailer);
            }
        }
    
        await _context.SaveChangesAsync();
    }
}