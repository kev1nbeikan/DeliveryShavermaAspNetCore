using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Configurations;

/// <summary>
/// Базовая конфигурация сущности для заказов.
/// </summary>
/// <typeparam name="T">Тип сущности заказа, наследующий от <see cref="BaseOrderEntity"/>.</typeparam>
public class BaseOrderConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseOrderEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        // Устанавливает первичный ключ.
        builder.HasKey(x => x.Id);

        builder.Property(b => b.ClientId)
            .IsRequired();

        builder.Property(b => b.CourierId)
            .IsRequired();

        builder.Property(b => b.StoreId)
            .IsRequired();

        // Устанавливает формат json.
        builder.Property(b => b.Basket)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(b => b.Price)
            .IsRequired();

        builder.Property(b => b.Comment)
            .HasMaxLength(BaseOrder.MaxCommentLength);

        builder.Property(b => b.Cheque)
            .HasMaxLength(BaseOrder.MaxChequeLength);
        
        // Создает индексы.
        builder
            .HasIndex(b => b.ClientId);

        builder
            .HasIndex(b => b.CourierId);

        builder
            .HasIndex(b => b.StoreId);
    }
}