using Moq;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;

namespace PinFood.Tests.Common.MockFactories;

public static class DateTimeFactory
{
	public static Mock<IDateTime> Create()
	{
		var dateTime = new DateTime(2024, 7, 17);

		return Create(dateTime);
	}
	
	public static Mock<IDateTime> Create(DateTime dateTime)
	{
		var dateTimeMock = new Mock<IDateTime>();
		dateTimeMock.Setup(m => m.Now).Returns(dateTime);

		return dateTimeMock;
	}
}