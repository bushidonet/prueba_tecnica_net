using Microsoft.EntityFrameworkCore;
using PruebaYevhenLetin.Controllers;
using PruebaYevhenLetin.Entity;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Retailer> Retailers { get; set; }

}