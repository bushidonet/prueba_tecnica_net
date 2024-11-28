using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaYevhenLetin;
using PruebaYevhenLetin.Entity;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1;
using WebApplication1.Data;
using Xunit;

public class RetailsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public RetailsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(); // Crear un cliente HTTP para la aplicación
    }

    [Fact]
    public async Task GetAll_ReturnsOkResponse_IsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/retails");

        // Assert: 200 OK
        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    // Test get all retailers
    [Fact]
    public async Task GetAll_ReturnsOkResponse_WithData()
    {
        //Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Retailers.Add(new Retailer { ReCode = "1234567890123", ReName = "Aalto Energia", Country = "FI", CodingScheme = "GS1" });
            context.Retailers.Add(new Retailer { ReCode = "9876543210987", ReName = "Example Corp", Country = "US", CodingScheme = "GS1" });
            context.SaveChanges();
        }
        
        // Act: 
        var response = await _client.GetAsync("/retails");
        var content = await response.Content.ReadAsStringAsync();
        var retailers = JsonSerializer.Deserialize<IEnumerable<Retailer>>(content);
        
        // Assert: retails hasn't empty
        Assert.NotEmpty(retailers);
    }

    // Test get a retails with primaryKey 
    [Fact]
    public async Task GetByReCode_ReturnsOkResponse_WhenExists()
    {
        //Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Retailers.Add(new Retailer
            {
                Id = 10000,
                ReCode = "3456789012345",
                ReName = "Retailer Three",
                Country = "FR",
                CodingScheme = "UPC"
            });
            context.SaveChanges();
        }

        // Act
        var response = await _client.GetAsync("/retails/10000");
        response.EnsureSuccessStatusCode();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };
        var json = await response.Content.ReadAsStringAsync();
        var retailer = JsonSerializer.Deserialize<Retailer>(json, options);
        
        //Assert: ReCode has same number
        Assert.Equal("3456789012345", retailer.ReCode); 
    }

    // Test get retailer with primary key don't exist
    [Fact]
    public async Task GetByReCode_ReturnsNotFound_WhenNotExists()
    {
        // Act
        var response = await _client.GetAsync("/retails/10000");

        // Assert: 404 Not Found
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
