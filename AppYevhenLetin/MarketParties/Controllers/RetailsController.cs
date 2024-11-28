using Microsoft.AspNetCore.Mvc;
using PruebaYevhenLetin.Entity;
using PruebaYevhenLetin.Services;

namespace PruebaYevhenLetin.Controllers;

[ApiController]
[Route("[controller]")]
public class RetailsController : ControllerBase
{
    private readonly IRetailer _retailer;

    public RetailsController(IRetailer retailerService)
    {
        _retailer = retailerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Retailer>>> GetAll()
    {
        try
        {
            var registros = await _retailer.GetAllRetailersAsync();
            return Ok(registros);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    [HttpGet("{primaryKey}")]
    public async Task<ActionResult<Retailer>> GetByReCode(int primaryKey)
    {
        try
        {
            var registro = await _retailer.GetRetailerByCodeAsync(primaryKey);

            if (registro is null)
            {
                return NotFound(new { Message = $"No se encontro primary key {primaryKey}" });
            }

            return Ok(registro);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}