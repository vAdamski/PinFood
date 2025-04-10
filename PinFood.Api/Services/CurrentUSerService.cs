using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PinFood.Application.Common.Interfaces.Api.Services;

namespace PinFood.Api.Services;

public class CurrentUserService : ICurrentUserService
{
	private const string NameIdentifierClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
	private const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

	public Guid Id { get; set; }
	public string Email { get; set; }
	public bool IsAuthenticated { get; set; }

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		var context = httpContextAccessor.HttpContext?.User;
		Id = GetUserId(context);
		Email = GetUserEmail(context);
		IsAuthenticated = !string.IsNullOrEmpty(Email);
	}

	private static Guid GetUserId(ClaimsPrincipal? user)
	{
		var idValue = user?.FindFirstValue(NameIdentifierClaim) ?? string.Empty;
		return Guid.TryParse(idValue, out var parsedId) ? parsedId : Guid.Empty;
	}

	private static string GetUserEmail(ClaimsPrincipal? user)
	{
		return user?.FindFirstValue(EmailClaim) ?? string.Empty;
	}
}