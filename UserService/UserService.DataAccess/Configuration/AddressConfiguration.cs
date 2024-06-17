using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasIndex(x => x.UserEntityId);
    }
}