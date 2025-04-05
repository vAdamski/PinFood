namespace PinFood.Api.Configurations;

public static class AppSettingsConfiguration
{
	public static WebApplicationBuilder ConfigureAppSettings(this WebApplicationBuilder builder)
	{
		builder.Configuration
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();
		
		return builder;
	}
}