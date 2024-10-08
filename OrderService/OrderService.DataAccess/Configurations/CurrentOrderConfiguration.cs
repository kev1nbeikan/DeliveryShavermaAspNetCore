﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;


namespace OrderService.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности для текцщих заказов.
/// </summary>
public class CurrentOrderConfiguration : IEntityTypeConfiguration<CurrentOrderEntity>
{
    public void Configure(EntityTypeBuilder<CurrentOrderEntity> builder)
    {
        new BaseOrderConfiguration<CurrentOrderEntity>().Configure(builder);

        builder.Property(b => b.Status)
            .IsRequired();

        builder.Property(b => b.StoreAddress)
            .HasMaxLength(BaseOrder.MaxAddressLength)
            .IsRequired();

        builder.Property(b => b.ClientAddress)
            .HasMaxLength(BaseOrder.MaxAddressLength)
            .IsRequired();
        
        builder.Property(b => b.CourierNumber)
            .HasMaxLength(BaseOrder.MaxNumberLength)
            .IsRequired();
        
        builder.Property(b => b.ClientNumber)
            .HasMaxLength(BaseOrder.MaxNumberLength)
            .IsRequired();

    }
}