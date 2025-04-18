using Microsoft.AspNetCore.Http;
using PinFood.Application.Actions.DishesActions.Shared;
using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.DishesActions.Commands.CreateDish;

public class CreateDishCommand : ICommand<Guid>
{
	public string Name { get; set; } = "";
	public string Description { get; set; } = "";
	
	public List<IFormFile> Images { get; set; } = new();
	public List<RecipeStepDto> RecipeSteps { get; set; } = new();
}