using AuthService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.DataAccess.Configuration;

public class CurierAuthConfiguration : IEntityTypeConfiguration<CurierAuthEntity>
{
    public void Configure(EntityTypeBuilder<CurierAuthEntity> builder)
    {
        builder.HasIndex(x => x.Login).IsUnique();
    }
}