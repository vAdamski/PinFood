using Serilog;
namespace PinFood.Api.Configurations;

public static class SerilogConfiguration
{
	public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder, IConfiguration configuration)
	{
		var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.Enrich.FromLogContext()
			.CreateLogger();
		
		builder.Logging.ClearProviders();
		builder.Logging.AddSerilog(logger);

		return builder;
	}
}