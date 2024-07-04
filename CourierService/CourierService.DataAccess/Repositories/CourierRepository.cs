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
			.Select(c => Courier.Create(c.Id, c.Status).Courier)
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

	public async Task<Guid> Delete(Guid id)
	{
		await _dbContext.Couriers
			.Where(c => c.Id == id)
			.ExecuteDeleteAsync();

		return id;
	}

	public async Task<Courier> GetById(Guid id)
	{
		var courier = await _dbContext.Couriers.FindAsync(id);
		
		return Courier.Create(courier.Id, courier.Status).Courier;
	}
}