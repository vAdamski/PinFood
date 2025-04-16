using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.DishesActions.Commands.DeleteDish;

public class DeleteDishCommand : ICommand
{
	public Guid Id { get; set; }
}