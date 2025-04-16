using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Domain.Entities;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishById;

public class DishViewModel
{
	public string Name { get; set; } = "";
	public string Description { get; set; } = "";
	public List<string> Images { get; set; } = new();
	public List<RecipeStepDto> RecipeSteps { get; set; } = new();
}