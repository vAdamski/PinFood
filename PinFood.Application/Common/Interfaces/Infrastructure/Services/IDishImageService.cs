using PinFood.Domain.Common;

namespace PinFood.Application.Common.Interfaces.Infrastructure.Services;

public interface IDishImageService
{
	Task<string> SaveImage(IFileData fileData);
}