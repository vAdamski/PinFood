using Microsoft.EntityFrameworkCore;
using Moq;
using PinFood.Persistence;
using PinFood.Tests.Common.Seeds;

namespace PinFood.Tests.Common.MockFactories;

public class AppDbContextMockFactory
{
	public static Mock<AppDbContext> Create()
	{
		var dateTimeMock = DateTimeFactory.Create(new DateTime(2024, 7, 17));
		
		var currentUserServiceMock = CurrentUserServiceMockFactory.Create();

		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;
		
		var mock = new Mock<AppDbContext>(options, dateTimeMock.Object, currentUserServiceMock.Object)
		{
			CallBase = true
		};
		
		var context = mock.Object;
		
		context.Database.EnsureCreated();

		context.Seed().Wait();

		return mock;
	}
	
	public static void Destroy(AppDbContext context)
	{
		context.Database.EnsureDeleted();
		context.Dispose();
	}
}