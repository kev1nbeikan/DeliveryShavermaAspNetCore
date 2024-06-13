using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Configurations
{
    public class LastOrderConfiguration : IEntityTypeConfiguration<LastOrderEnity>
    {
        public void Configure(EntityTypeBuilder<LastOrderEnity> builder)
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
                .HasMaxLength(Order.MAX_COMMENT_LENGHT);

            builder.Property(b => b.CourierNumber)
                .HasMaxLength(Order.MAX_NUMBER_LENGHT)
                .IsRequired();

            builder.Property(b => b.ClientNumber)
                .HasMaxLength(Order.MAX_COMMENT_LENGHT)
                .IsRequired();

            builder.Property(b => b.ClientAddress)
                .HasMaxLength(Order.MAX_ADDRESS_LENGHT)
                .IsRequired();

            //cheque grade
        }
    }
}
