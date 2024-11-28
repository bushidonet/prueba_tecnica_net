using System.Net;
using Newtonsoft.Json;
using PruebaYevhenLetin.Entity;

namespace PruebaYevhenLetin.Services;

public class ApiService: IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _retailersApiUrl;
    
    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _retailersApiUrl = config["ApiSettings:RetailersApiUrl"] ?? string.Empty;
    }
    
    public async Task<IEnumerable<Retailer>> GetAllRetailersFromApiAsync()
    {
        try
        {
            // GET all retailers from api
            var response = await _httpClient.GetAsync(_retailersApiUrl);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync();
                    var retailers = JsonConvert.DeserializeObject<IEnumerable<Retailer>>(content);
                    return retailers ?? Enumerable.Empty<Retailer>();

                case HttpStatusCode.NoContent:
                    Console.WriteLine("No content available (204). Returning empty list.");
                    return Enumerable.Empty<Retailer>();

                case HttpStatusCode.BadRequest:
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error 400: {errorContent}");
                    return Enumerable.Empty<Retailer>();
                default:
                    var defaultErrorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error {response.StatusCode}: {defaultErrorContent}");
                    return Enumerable.Empty<Retailer>();
            }
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"Exception: {ex.Message}");
            return Enumerable.Empty<Retailer>(); 
        }

    }
    
}