using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;

namespace OrderService.DataAccess.Configurations
{
    public class LastOrderConfiguration : IEntityTypeConfiguration<LastOrderEntity>
    {
        public void Configure(EntityTypeBuilder<LastOrderEntity> builder)
        {
            new BaseOrderConfiguration<LastOrderEntity>().Configure(builder);
        }
    }
}