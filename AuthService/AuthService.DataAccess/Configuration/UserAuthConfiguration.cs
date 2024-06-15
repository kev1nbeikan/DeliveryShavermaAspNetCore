using AuthService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.DataAccess.Configuration;

public class UserAuthConfiguration : IEntityTypeConfiguration<UserAuthEntity>
{
    public void Configure(EntityTypeBuilder<UserAuthEntity> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
    }
}