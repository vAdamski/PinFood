using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.DishesActions.Commands.RemoveFromFavorites;

public class RemoveFromFavoritesCommand : ICommand
{
	public Guid DishId { get; set; }
}