using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Domain.Common;

namespace PinFood.Application.Actions.UsersActions.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
	public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}