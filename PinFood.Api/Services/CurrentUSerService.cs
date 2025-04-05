using System.Security.Claims;
using PinFood.Application.Common.Interfaces.Api.Services;

namespace PinFood.Api.Services;

public class CurrentUserService : ICurrentUserService
{
	public string Email { get; set; }
	public bool IsAuthenticated { get; set; }

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("Email") ?? string.Empty;

		IsAuthenticated = !string.IsNullOrEmpty(Email);
	}
}