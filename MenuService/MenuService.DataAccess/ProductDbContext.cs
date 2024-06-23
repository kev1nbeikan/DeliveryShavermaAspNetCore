using MenuService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuService.DataAccess;

public class ProductDbContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; } = null!;

	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}
}