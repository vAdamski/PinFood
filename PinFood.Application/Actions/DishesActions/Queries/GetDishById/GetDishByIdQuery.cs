using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishById;

public class GetDishByIdQuery : IQuery<DishViewModel>
{
	public Guid Id { get; set; }
}