using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.DataAccess.Configurations
{
    public class CanceledOrderConfiguration : IEntityTypeConfiguration<CanceledOrderEntity>
    {
        public void Configure(EntityTypeBuilder<CanceledOrderEntity> builder)
        {
            new BaseOrderConfiguration<CanceledOrderEntity>().Configure(builder);

            builder.Property(b => b.LastStatus)
                .IsRequired();

            builder.Property(b => b.ReasonOfCanceled)
                .HasMaxLength(BaseOrder.MaxCommentLength);
        }
    }
}