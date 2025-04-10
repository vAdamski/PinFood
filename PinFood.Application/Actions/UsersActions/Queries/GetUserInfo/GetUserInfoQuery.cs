using PinFood.Application.Actions.UsersActions.Queries.Shared;
using PinFood.Application.Common.Abstraction.Messaging;

namespace PinFood.Application.Actions.UsersActions.Queries.GetUserInfo;

public class GetUserInfoQuery : IQuery<UserInfoViewModel>
{
	public Guid UserId { get; set; }
}