using CourierService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.DataAccess.Configurations;

public class CourierConfiguration : IEntityTypeConfiguration<CourierEntity>
{
	public void Configure(EntityTypeBuilder<CourierEntity> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Status);
	}
}