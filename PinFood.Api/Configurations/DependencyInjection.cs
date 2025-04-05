using Microsoft.Extensions.DependencyInjection.Extensions;
using PinFood.Api.Services;
using PinFood.Application.Common.Interfaces.Api.Services;

namespace PinFood.Api.Configurations;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
		
		return services;
	}
}