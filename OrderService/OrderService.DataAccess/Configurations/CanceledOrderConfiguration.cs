using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;
using OrderService.Domain.Models.Code;

namespace OrderService.DataAccess.Configurations
{
    public class CanceledOrderConfiguration : IEntityTypeConfiguration<CanceledOrderEntity>
    {
        public void Configure(EntityTypeBuilder<CanceledOrderEntity> builder)
        {
            builder.Property(b => b.LastStatus)
                .IsRequired();

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_CanceledOrder_LastStatus",
                    $"\"LastStatus\" >= 1 AND \"LastStatus\" < {(int)StatusCode.Accepted}"));

            builder.Property(b => b.ReasonOfCanceled)
                .HasMaxLength(BaseOrder.MaxCommentLength);
        }
    }
}