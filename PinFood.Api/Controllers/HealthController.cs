using MediatR;
using Microsoft.AspNetCore.Mvc;
using PinFood.Application.Actions.HealthActions.Queries.CheckHealth;

namespace PinFood.Api.Controllers;

[Route("api/health")]
public class HealthController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var result = await sender.Send(new CheckHealthQuery());
		return Ok(result);
	}
}