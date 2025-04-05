using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PinFood.Application.Common.Behaviours;
using PinFood.Application.Common.Interfaces.Application.Providers;
using PinFood.Application.Common.Interfaces.Application.Services;
using PinFood.Application.Providers;
using PinFood.Application.Services;

namespace PinFood.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

		services.AddTransient<IPasswordHasher, PasswordHasher>();
		services.AddTransient<ITokenProvider, TokenProvider>();
        
		return services;
	}
}