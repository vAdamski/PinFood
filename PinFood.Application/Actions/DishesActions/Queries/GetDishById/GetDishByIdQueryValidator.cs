using FluentValidation;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishById;

public class GetDishByIdQueryValidator : AbstractValidator<GetDishByIdQuery>
{
	public GetDishByIdQueryValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("Dish Id cannot be empty.");
	}
}