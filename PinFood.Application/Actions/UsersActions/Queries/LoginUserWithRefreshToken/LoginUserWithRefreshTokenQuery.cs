using PinFood.Application.Actions.UsersActions.Queries.Shared;
using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

public class LoginUserWithRefreshTokenQuery : IQuery<LoginUserResponse>
{
	public string RefreshToken { get; set; } = string.Empty;
}