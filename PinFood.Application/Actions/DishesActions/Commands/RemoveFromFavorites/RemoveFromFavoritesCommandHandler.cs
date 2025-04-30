using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.DishesActions.Commands.RemoveFromFavorites;

public class RemoveFromFavoritesCommandHandler(
	IFavoriteDishesRepository favoriteDishesRepository,
	ICurrentUserService currentUserService) : ICommandHandler<RemoveFromFavoritesCommand>
{
	public async Task<Result> Handle(RemoveFromFavoritesCommand request, CancellationToken cancellationToken)
	{
		var favoriteDish = await favoriteDishesRepository.GetByIdAsync(request.DishId, cancellationToken);

		if (favoriteDish == null || favoriteDish.Email != currentUserService.Email)
			return Result.Failure(DomainErrors.Dish.NotFound);

		await favoriteDishesRepository.RemoveAsync(favoriteDish, cancellationToken);

		return Result.Success();
	}
}