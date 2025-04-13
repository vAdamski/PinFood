using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PinFood.Application.Common.Interfaces.Application.Providers;

namespace PinFood.Application.Providers;

public class FileUrlProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
	: IFileUrlProvider
{
	private readonly string _staticFileRequestPath = configuration["FileStorage:StaticFileRequestPath"]!;

	public string GenerateFileUrl(string filePath)
	{
		var request = httpContextAccessor.HttpContext?.Request 
		              ?? throw new InvalidOperationException("HttpContext is not available.");

		var baseUrl = $"{request.Scheme}://{request.Host}";
		return $"{baseUrl}{_staticFileRequestPath}/{filePath}";
	}
}