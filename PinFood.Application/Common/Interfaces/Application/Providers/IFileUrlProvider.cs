namespace PinFood.Application.Common.Interfaces.Application.Providers;

public interface IFileUrlProvider
{
	string GenerateFileUrl(string filePath);
}