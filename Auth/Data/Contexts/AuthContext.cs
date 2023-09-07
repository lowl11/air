using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Contexts;

public sealed class AuthContext : DbContext
{

    public DbSet<RoleEntity> Roles { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().ToTable("roles").
            HasAlternateKey(role => role.Code);
        modelBuilder.Entity<UserEntity>().ToTable("users").
            HasAlternateKey(user => user.Username);
    }
    
}