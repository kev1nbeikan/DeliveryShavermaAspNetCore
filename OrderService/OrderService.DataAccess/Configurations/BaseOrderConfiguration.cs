using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Order;

namespace OrderService.DataAccess.Configurations;

public class BaseOrderConfiguration<T> : IEntityTypeConfiguration<T> 
    where T : BaseOrderEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
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

        builder.Property(b => b.Price)
            .IsRequired();

        builder.Property(b => b.Comment)
            .HasMaxLength(BaseOrder.MaxCommentLength);

        builder.Property(b => b.Cheque)
            .HasMaxLength(BaseOrder.MaxChequeLength);
    }
}