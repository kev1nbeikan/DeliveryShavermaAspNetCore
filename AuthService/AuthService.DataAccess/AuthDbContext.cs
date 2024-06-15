using AuthService.DataAccess.Configuration;
using AuthService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<CurierAuthEntity> CurierAuth { get; set; }
    public DbSet<StoreAuthEntity> StoreAuth { get; set; }
    public DbSet<UserAuthEntity> UsersAuth { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CurierAuthConfiguration());
        modelBuilder.ApplyConfiguration(new StoreAuthConfiguration());
        modelBuilder.ApplyConfiguration(new UserAuthConfiguration());
    }
}