using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.DishesActions.Commands.AddToFavorite;

public class AddToFavoritesCommandHandler(
	IDishesRepository dishesRepository,
	ICurrentUserService currentUserService,
	IFavoriteDishesRepository favoriteDishesRepository)
	: ICommandHandler<AddToFavoritesCommand, Guid>
{
	public async Task<Result<Guid>> Handle(AddToFavoritesCommand request, CancellationToken cancellationToken)
	{
		var dish = await dishesRepository.GetByIdAsync(request.DishId, cancellationToken);

		if (dish == null)
			return Result.Failure<Guid>(DomainErrors.Dish.NotFound);

		var favoriteDish = FavoriteDish.AddToFavorites(dish, currentUserService.Email);

		if (favoriteDish.IsFailure)
			return Result.Failure<Guid>(favoriteDish.Error);

		await favoriteDishesRepository.AddAsync(favoriteDish.Value, cancellationToken);

		return Result.Success(favoriteDish.Value.Id);
	}
}