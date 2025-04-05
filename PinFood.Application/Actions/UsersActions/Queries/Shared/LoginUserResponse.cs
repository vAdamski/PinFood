namespace PinFood.Application.Actions.UsersActions.Queries.Shared;

public class LoginUserResponse
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
}