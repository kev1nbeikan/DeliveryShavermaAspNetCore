using CourierService.Core.Abstractions;
using CourierService.Core.Models;
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
			.Select(c => Courier.Create(c.Id, c.Email, c.Password, c.Status).Courier)
			.ToList();

		return couriers;
	}

	public async Task<Guid> Create(Courier courier)
	{
		var courierEntity = new CourierEntity()
		{
			Id = courier.Id,
			Email = courier.Email,
			Password = courier.Password
		};

		await _dbContext.Couriers.AddAsync(courierEntity);
		await _dbContext.SaveChangesAsync();

		return courierEntity.Id;
	}

	public async Task<Guid> Update(Guid id, string email, string password)
	{
		await _dbContext.Couriers.Where(c => c.Id == id)
			.ExecuteUpdateAsync(
				s => s
					.SetProperty(c => c.Email, c => email)
					.SetProperty(c => c.Password, c => password)
			);

		return id;
	}

	public async Task<Guid> Update(Guid id, bool status)
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
}