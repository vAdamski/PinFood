using Moq;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Persistence;
using PinFood.Tests.Common.MockFactories;

namespace PinFood.Tests.Common;

public class CommandTestBase : IDisposable
{
	protected readonly AppDbContext _context;
	protected readonly Mock<AppDbContext> _contextMock;
	protected readonly ICurrentUserService _currentUserService;
	protected readonly Mock<ICurrentUserService> _currentUserServiceMock;

	
	public CommandTestBase()
	{
		_contextMock = AppDbContextMockFactory.Create();
		_currentUserServiceMock = CurrentUserServiceMockFactory.Create();
		_context = _contextMock.Object;
		_currentUserService = _currentUserServiceMock.Object;
	}
	
	public void Dispose()
	{
		AppDbContextMockFactory.Destroy(_contextMock.Object);
	}
}