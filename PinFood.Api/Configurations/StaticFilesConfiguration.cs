using Microsoft.Extensions.FileProviders;

namespace PinFood.Api.Configurations;

public static class StaticFilesConfiguration
{
	public static IApplicationBuilder UseStaticFilesConfiguration(this IApplicationBuilder app, IConfiguration configuration)
	{
		var basePath = configuration["FileStorage:BasePath"];

		if (!string.IsNullOrEmpty(basePath) && Directory.Exists(basePath))
		{
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(basePath),
				RequestPath = "/files"
			});
		}
		
		return app;
	}
}