namespace PinFood.Application.Actions.DishesActions.Commands.CreateDish;

public class RecipeStepDto
{
	public int Order { get; set; }
	public string Name { get; set; } = "";
	public string Description { get; set; } = "";
}