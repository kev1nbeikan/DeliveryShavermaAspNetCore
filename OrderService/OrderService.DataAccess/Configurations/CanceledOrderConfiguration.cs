using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Configurations
{
    public class CanceledOrderConfiguration: IEntityTypeConfiguration<CanceledOrderEntity>
    {
        public void Configure(EntityTypeBuilder<CanceledOrderEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.ClientId)
                .IsRequired();

            builder.Property(b => b.CourierId)
                .IsRequired();

            builder.Property(b => b.StoreId)
                .IsRequired();

            builder.Property(b => b.Basket)
                .HasColumnType("jsonb")
                .IsRequired();

            builder.Property(b => b.LastStatus)
                .IsRequired();

            builder.ToTable(b => b.HasCheckConstraint("CK_CurrentOrder_Status", "LastStatus >= 0 AND LastStatus <= 6"));

            builder.Property(b => b.ReasonOfCanceled)
                .HasMaxLength(LastOrder.MaxCommentLength);

            builder.Property(b => b.Price)
                .IsRequired();

            builder.Property(b => b.Comment)
                .HasMaxLength(LastOrder.MaxCommentLength);

            builder.Property(b => b.CourierNumber)
                .HasMaxLength(LastOrder.MaxNumberLength)
                .IsRequired();

            builder.Property(b => b.ClientNumber)
                .HasMaxLength(LastOrder.MaxCommentLength)
                .IsRequired();

            builder.Property(b => b.ClientAddress)
                .HasMaxLength(LastOrder.MaxAddressLength)
                .IsRequired();
        }
    }
}
