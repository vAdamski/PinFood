using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.HealthActions.Queries.CheckHealth;

namespace PinFood.Api.Controllers;

[Route("api/health")]
public class HealthController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Get()
	{
		var result = await sender.Send(new CheckHealthQuery());

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}