using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;
using OrderService.Domain.Models.Order;

namespace OrderService.DataAccess.Configurations
{
    public class CurrentOrderConfiguration : IEntityTypeConfiguration<CurrentOrderEntity>
    {
        public void Configure(EntityTypeBuilder<CurrentOrderEntity> builder)
        {
            new BaseOrderConfiguration<CurrentOrderEntity>().Configure(builder);

            builder.Property(b => b.Status)
                .IsRequired();

            builder.ToTable(b =>
                b.HasCheckConstraint("CK_CurrentOrder_Status",
                    $"\"Status\" >= 0 AND \"Status\" < {(int)StatusCode.Accepted}"));

            builder.Property(b => b.StoreAddress)
                .HasMaxLength(BaseOrder.MaxAddressLength)
                .IsRequired();

            builder.Property(b => b.CourierNumber)
                .HasMaxLength(BaseOrder.MaxNumberLength)
                .IsRequired();

            builder.Property(b => b.ClientNumber)
                .HasMaxLength(BaseOrder.MaxCommentLength)
                .IsRequired();

            builder.Property(b => b.ClientAddress)
                .HasMaxLength(BaseOrder.MaxAddressLength)
                .IsRequired();
        }
    }
}