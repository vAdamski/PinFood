using Microsoft.EntityFrameworkCore;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Repositories;

public class FavoriteDishesRepository(IAppDbContext ctx) : IFavoriteDishesRepository
{
	public async Task<FavoriteDish?> GetByIdAsync(Guid requestDishId, CancellationToken cancellationToken)
	{
		return await ctx.FavoriteDishes
			.AsNoTracking()
			.FirstOrDefaultAsync(f => f.Id == requestDishId, cancellationToken);
	}

	public async Task<List<FavoriteDish>> GetFavoriteDishesAsync(string email, CancellationToken cancellationToken)
	{
		return await ctx.FavoriteDishes
			.Where(f => f.Email == email)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
	
	public async Task<Guid> AddAsync(FavoriteDish favoriteDish, CancellationToken cancellationToken)
	{
		await ctx.FavoriteDishes.AddAsync(favoriteDish, cancellationToken);
		await ctx.SaveChangesAsync(cancellationToken);

		return favoriteDish.Id;
	}

	public async Task RemoveAsync(FavoriteDish favoriteDish, CancellationToken cancellationToken)
	{
		ctx.FavoriteDishes.Remove(favoriteDish);
		await ctx.SaveChangesAsync(cancellationToken);
	}
}