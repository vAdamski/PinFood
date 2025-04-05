using Microsoft.Extensions.DependencyInjection;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Infrastructure.Services;

namespace PinFood.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IDateTime, DateTimeService>();

		return services;
	}
}