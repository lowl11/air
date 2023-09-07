using Aircraft.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aircraft.Data.Contexts;

public sealed class AircraftContext : DbContext
{

    public DbSet<FlightEntity> Flights { get; set; } = null!;

    public AircraftContext(DbContextOptions<AircraftContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlightEntity>().ToTable("flights");
    }
    
}