using PruebaYevhenLetin.Entity;

namespace PruebaYevhenLetin.Services;

public interface IRetailer
{
    Task<IEnumerable<Retailer>> GetAllRetailersAsync();
    Task<Retailer> GetRetailerByCodeAsync(int primaryKey);
}