using AuthService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.DataAccess.Configuration;

public class StoreAuthConfiguration : IEntityTypeConfiguration<StoreAuthEntity>
{
    public void Configure(EntityTypeBuilder<StoreAuthEntity> builder)
    {
        builder.HasIndex(x => x.Login).IsUnique();
    }
}