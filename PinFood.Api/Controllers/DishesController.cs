using MediatR;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.DishesActions.Commands.AddToFavorite;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Application.Actions.DishesActions.Commands.DeleteDish;
using PinFood.Application.Actions.DishesActions.Commands.RemoveFromFavorites;
using PinFood.Application.Actions.DishesActions.Queries.GetDishById;
using PinFood.Application.Actions.DishesActions.Queries.GetDishes;

namespace PinFood.Api.Controllers;

[Route("api/dishes")]
public class DishesController(ISender sender) : BaseController(sender)
{
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		var result = await sender.Send(new GetDishByIdQuery { Id = id });
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var result = await sender.Send(new GetDishesQuery());
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost]
	public async Task<IActionResult> Create([FromForm] CreateDishCommand command)
	{
		var result = await sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost("{dishId}/like")]
	public async Task<IActionResult> AddToFavorites(Guid dishId)
	{
		var result = await sender.Send(new AddToFavoritesCommand { DishId = dishId });
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpDelete("{dishId}/unlike")]
	public async Task<IActionResult> RemoveFromFavorites(Guid dishId)
	{
		var result = await sender.Send(new RemoveFromFavoritesCommand { DishId = dishId });
		
		return result.IsSuccess ? NoContent() : HandleFailure(result);
	}
	
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await sender.Send(new DeleteDishCommand { Id = id });
		
		return result.IsSuccess ? NoContent() : HandleFailure(result);
	}
}