using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

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