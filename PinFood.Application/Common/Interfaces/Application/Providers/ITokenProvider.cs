using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Application.Providers;

public interface ITokenProvider
{
	string CreateJwtToken(User user);
	RefreshToken CreateRefreshToken(User user);
}