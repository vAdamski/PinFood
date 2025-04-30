using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.DishesActions.Commands.AddToFavorite;

public class AddToFavoritesCommand : ICommand<Guid>
{
	public Guid DishId { get; set; }
}