using PinFood.Application.Common.Interfaces.Infrastructure.Services;

namespace PinFood.Infrastructure.Services;

public class DateTimeService : IDateTime
{
	public DateTime Now => DateTime.Now;
}