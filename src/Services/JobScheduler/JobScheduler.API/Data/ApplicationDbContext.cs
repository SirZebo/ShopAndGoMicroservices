using JobScheduler.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobScheduler.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
}