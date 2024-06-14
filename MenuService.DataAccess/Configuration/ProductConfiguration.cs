
using MenuService.Core.Models;
using MenuService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuService.DataAccess.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
	public void Configure(EntityTypeBuilder<ProductEntity> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Title)
			.HasMaxLength(Product.MAX_TITLE_LENGTH)
			.IsRequired();

		builder.Property(p => p.Description)
			.HasMaxLength(Product.MAX_DESCRIPTION_LENGTH)
			.IsRequired();

		builder.Property(p => p.Composition)
			.HasMaxLength(Product.MAX_COMPOSITION_LENGTH)
			.IsRequired();

		builder.Property(p => p.Price)
			.IsRequired();

		builder.Property(p => p.ImagePath)
			.IsRequired();
	}
}