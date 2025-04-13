using Microsoft.Extensions.FileProviders;

namespace PinFood.Api.Configurations;

public static class StaticFilesConfiguration
{
	public static IApplicationBuilder UseStaticFilesConfiguration(this IApplicationBuilder app, IConfiguration configuration)
	{
		var basePath = configuration["FileStorage:BasePath"]!;
		var staticFileRequestPath = configuration["FileStorage:StaticFileRequestPath"]!;
		
		if (string.IsNullOrEmpty(basePath))
			throw new ArgumentNullException(nameof(basePath), "Base path cannot be null or empty.");
		
		if (string.IsNullOrEmpty(staticFileRequestPath))
			throw new ArgumentNullException(nameof(staticFileRequestPath), "Static file request path cannot be null or empty.");
		
		if (!Directory.Exists(basePath))
			throw new DirectoryNotFoundException($"The directory '{basePath}' does not exist.");

		app.UseStaticFiles(new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(basePath),
			RequestPath = staticFileRequestPath
		});
		
		return app;
	}
}