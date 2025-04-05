using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.UsersActions.Commands.RegisterUser;

public class RegisterUserCommand : ICommand
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}