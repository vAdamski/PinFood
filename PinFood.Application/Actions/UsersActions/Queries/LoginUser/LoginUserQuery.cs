using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.UsersActions.Queries.LoginUser;

public class LoginUserQuery : IQuery<LoginUserResponse>
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}