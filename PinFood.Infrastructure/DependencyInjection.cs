using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Infrastructure.Services;

namespace PinFood.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var basePath = configuration.GetValue<string>("FileStorage:BasePath");
		services.AddSingleton<IFileStorage>(new LocalFileStorage(basePath!));
		
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddTransient<IDishImageService, DishImageService>();

		return services;
	}
}