using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.UsersActions.Commands.RegisterUser;
using PinFood.Application.Actions.UsersActions.Queries.GetCurrentUserInfo;
using PinFood.Application.Actions.UsersActions.Queries.GetUserInfo;
using PinFood.Application.Actions.UsersActions.Queries.LoginUser;
using PinFood.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

namespace PinFood.Api.Controllers;

[Route("api/users")]
public class UsersController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	public async Task<IActionResult> GetCurrentUser()
	{
		var result = await Sender.Send(new GetCurrentUserInfoQuery());
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetUser([FromRoute] Guid userId)
	{
		var result = await Sender.Send(new GetUserInfoQuery()
		{
			UserId = userId
		});
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
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