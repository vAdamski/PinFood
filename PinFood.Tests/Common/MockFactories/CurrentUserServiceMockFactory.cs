using Moq;
using PinFood.Application.Common.Interfaces.Api.Services;

namespace PinFood.Tests.Common.MockFactories;

public static class CurrentUserServiceMockFactory
{
	public static readonly Guid UserId = new Guid("00000000-0000-0000-0000-000000000001");
	public static readonly string UserEmail = "test@pinfood.com";

	public static Mock<ICurrentUserService> Create()
	{
		var currentUserServiceMock = new Mock<ICurrentUserService>();
		currentUserServiceMock.Setup(m => m.Email).Returns(UserEmail);
		currentUserServiceMock.Setup(m => m.Id).Returns(UserId);

		return currentUserServiceMock;
	}
}