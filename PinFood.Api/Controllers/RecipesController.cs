using MediatR;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Application.Actions.DishesActions.Queries.GetDishById;

namespace PinFood.Api.Controllers;

[Route("api/recipes")]
public class RecipesController(ISender sender) : BaseController(sender)
{
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		var result = await sender.Send(new GetDishByIdQuery { Id = id });
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateDishCommand command)
	{
		var result = await sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}