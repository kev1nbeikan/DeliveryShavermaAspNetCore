using Delivery.ProductAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace Delivery.ProductAPI.Data;

public class DeliveryAppDbContext : DbContext
{
	public DbSet<Product> Items { get; set; } = null!;

	public DeliveryAppDbContext(DbContextOptions<DeliveryAppDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}
}