using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.UsersActions.Commands.RegisterUser;
using PinFood.Application.Actions.UsersActions.Queries.LoginUser;
using PinFood.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

namespace PinFood.Api.Controllers;

[Route("api/users")]
public class UsersController(ISender sender) : BaseController(sender)
{
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetUser([FromRoute] Guid userId)
	{
		return Ok(userId);
	}
	
	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
	{
		var result = await Sender.Send(command);

		return result.IsSuccess ? Ok() : HandleFailure(result);
	}
	
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
	{
		var result = await Sender.Send(query);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken([FromBody] LoginUserWithRefreshTokenQuery query)
	{
		var result = await Sender.Send(query);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		throw new NotImplementedException();
	}
}