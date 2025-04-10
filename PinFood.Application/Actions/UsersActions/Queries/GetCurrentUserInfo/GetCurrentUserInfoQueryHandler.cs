using PinFood.Application.Actions.UsersActions.Queries.Shared;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.UsersActions.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
	: IQueryHandler<GetCurrentUserInfoQuery, UserInfoViewModel>
{
	public async Task<Result<UserInfoViewModel>> Handle(GetCurrentUserInfoQuery request,
		CancellationToken cancellationToken)
	{
		var userId = currentUserService.Id;

		var user = await userRepository.GetByIdAsync(userId, cancellationToken);

		if (user == null)
			return Result.Failure<UserInfoViewModel>(DomainErrors.User.UserNotFound);

		var userInfo = new UserInfoViewModel
		{
			Id = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email
		};

		return Result.Success(userInfo);
	}
}