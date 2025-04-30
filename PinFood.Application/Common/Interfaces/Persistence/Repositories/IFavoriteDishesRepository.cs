using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Persistence.Repositories;

public interface IFavoriteDishesRepository
{
	Task<FavoriteDish?> GetByIdAsync(Guid requestDishId, CancellationToken cancellationToken);
	Task<List<FavoriteDish>> GetFavoriteDishesAsync(string email, CancellationToken cancellationToken);
	Task<Guid> AddAsync(FavoriteDish favoriteDish, CancellationToken cancellationToken);
	Task RemoveAsync(FavoriteDish favoriteDish, CancellationToken cancellationToken);
}