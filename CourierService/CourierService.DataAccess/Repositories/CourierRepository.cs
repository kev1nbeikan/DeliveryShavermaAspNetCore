﻿using BarsGroupProjectN1.Core.Models.Courier;
using CourierService.Core.Abstractions;
using CourierService.Core.Models;
using CourierService.Core.Models.Code;
using CourierService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierService.DataAccess.Repositories;

public class CourierRepository : ICourierRepository
{
    private readonly CourierDbContext _dbContext;

    public CourierRepository(CourierDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Courier>> Get()
    {
        var courierEntities = await _dbContext.Couriers.ToListAsync();

        var couriers = courierEntities
            .Select(c => Courier.Create(c.Id, c.Status, c.ActiveOrdersCount, c.PhoneNumber).Courier)
            .ToList();

        return couriers;
    }

    public async Task<Guid> Create(Courier courier)
    {
        var courierEntity = new CourierEntity()
        {
            Id = courier.Id,
            Status = courier.Status
        };

        await _dbContext.Couriers.AddAsync(courierEntity);
        await _dbContext.SaveChangesAsync();

        return courierEntity.Id;
    }

    public async Task<Guid> Update(Guid id, CourierStatusCode status)
    {
        await _dbContext.Couriers.Where(c => c.Id == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.Status, c => status)
            );
        return id;
    }

    public async Task<bool> Update(Guid id, string phoneNumber)
    {
        await _dbContext.Couriers.Where(c => c.Id == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.PhoneNumber, c => phoneNumber)
            );
        return true;
    }

    public async Task AdjustActiveOrdersCount(Guid id, int adjustment)
    {
        Console.WriteLine(adjustment);
        await _dbContext.Couriers
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(
                        c => c.ActiveOrdersCount,
                        c => c.ActiveOrdersCount + adjustment)
            );
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _dbContext.Couriers
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<Courier?> GetById(Guid id)
    {
        var courier = await _dbContext.Couriers.FindAsync(id);

        return courier is null
            ? null
            : Courier.Create(courier.Id, courier.Status, courier.ActiveOrdersCount, courier.PhoneNumber).Courier;
    }
}