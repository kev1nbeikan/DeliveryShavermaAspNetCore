﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.DataAccess.Entities;
using OrderService.Domain.Models;

namespace OrderService.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности для отмененных заказов.
/// </summary>
public class CanceledOrderConfiguration : IEntityTypeConfiguration<CanceledOrderEntity>
{
    public void Configure(EntityTypeBuilder<CanceledOrderEntity> builder)
    {
        new BaseOrderConfiguration<CanceledOrderEntity>().Configure(builder);

        builder.Property(b => b.LastStatus)
            .IsRequired();

        builder.Property(b => b.ReasonOfCanceled)
            .HasMaxLength(BaseOrder.MaxCommentLength);

        builder.Property(b => b.CanceledDate)
            .IsRequired();
    }
}