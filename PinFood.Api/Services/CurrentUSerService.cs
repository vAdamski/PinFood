using System.Security.Claims;
using PinFood.Application.Common.Interfaces.Api.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PinFood.Api.Services;

public class CurrentUserService : ICurrentUserService
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public bool IsAuthenticated { get; set; }

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		var id = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? string.Empty;
		Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Email) ?? string.Empty;
		Id = Guid.TryParse(id, out var parsedId) ? parsedId : throw new InvalidCastException();

		IsAuthenticated = !string.IsNullOrEmpty(Email);
	}
}