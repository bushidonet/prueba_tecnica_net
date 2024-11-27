using Microsoft.AspNetCore.Mvc;

namespace PruebaYevhenLetin.Controllers;

[ApiController]
[Route("[controller]")]
public class RetailsController : ControllerBase
{
    private readonly ILogger<RetailsController> _logger;

    public RetailsController()
    {

    }

    [HttpGet(Name = "Get")]
    public IEnumerable<Retailer> Get()
    {
        return;
    }
}