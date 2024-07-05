using CourierService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierService.DataAccess;

public class CourierDbContext : DbContext
{
	public DbSet<CourierEntity> Couriers { get; set; }

	public CourierDbContext(DbContextOptions<CourierDbContext> options) : base(options)
	{
	}
}