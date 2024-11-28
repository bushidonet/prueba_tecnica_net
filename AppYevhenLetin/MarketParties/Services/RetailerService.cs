using Microsoft.EntityFrameworkCore;
using PruebaYevhenLetin.Entity;
using WebApplication1.Data;

namespace PruebaYevhenLetin.Services;

public class RetailerService(AppDbContext data) : IRetailer
{
    private IRetailer _retailerImplementation;

    public async Task<IEnumerable<Retailer>> GetAllRetailersAsync()
    {
        return await data.Retailers.ToListAsync();
    }

  

    public async Task<Retailer> GetRetailerByCodeAsync(int primaryKey)
    {
        return await data.Retailers.FirstOrDefaultAsync(r => r.Id == primaryKey);
    }

   
}