using MediatR;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;

namespace PinFood.Api.Controllers;

[Route("api/recipes")]
public class RecipesController(ISender sender) : BaseController(sender)
{
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		throw new NotImplementedException();
	}
	
	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateDishCommand command)
	{
		var result = await sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}