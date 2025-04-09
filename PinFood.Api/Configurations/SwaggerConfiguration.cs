using Microsoft.OpenApi.Models;

namespace PinFood.Api.Configurations;

public static class SwaggerConfiguration
{
	public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "PinFood API",
				Version = "v1"
			});

			// Konfiguracja JWT
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "bearer", // Ustaw automatyczne użycie schematu "Bearer"
				BearerFormat = "JWT", // Informacja, że używany jest JWT
				In = ParameterLocation.Header,
				Description = "Wprowadź token JWT w formacie bez 'Bearer'. Prefiks zostanie dodany automatycznie."
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] {}
				}
			});
		});


		return services;
	}
	
	public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "PinFood API V1");
			c.RoutePrefix = string.Empty; // Swagger jest dostępny pod adresem root URL (np., /)
		});

		return app;
	}
}