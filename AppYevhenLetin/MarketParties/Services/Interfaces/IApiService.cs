using PruebaYevhenLetin.Entity;

namespace PruebaYevhenLetin.Services;

public interface IApiService
{
    Task<IEnumerable<Retailer>> GetAllRetailersFromApiAsync();
}