using PinFood.Application.Actions.DishesActions.Shared;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishes;

public class DishListItemDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = "";
	public string Description { get; set; } = "";
	public List<string> Images { get; set; } = new();
	public List<RecipeStepDto> RecipeSteps { get; set; } = new();
}