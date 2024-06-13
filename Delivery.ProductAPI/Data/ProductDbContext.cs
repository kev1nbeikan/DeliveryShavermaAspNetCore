using Delivery.ProductAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.ProductAPI.Data;

public class ProductDbContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; } = null!;

	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}
}