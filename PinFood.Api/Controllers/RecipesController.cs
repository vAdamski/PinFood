using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PinFood.Api.Controllers;

[Route("api/recipes")]
public class RecipesController(ISender sender) : BaseController(sender)
{
	
}