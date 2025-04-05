using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PinFood.Api.Controllers;

[Route("api/users")]
public class UsersController(ISender sender) : BaseController(sender)
{
	[HttpGet("{userId:guid}")]
	public async Task<IActionResult> GetUser(Guid userId)
	{
		throw new NotImplementedException();
	}
	
	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> Register()
	{
		throw new NotImplementedException();
	}
	
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> Login()
	{
		throw new NotImplementedException();
	}
	
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken()
	{
		throw new NotImplementedException();
	}
	
	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		throw new NotImplementedException();
	}
}