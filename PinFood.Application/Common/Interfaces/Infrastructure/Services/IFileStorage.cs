using PinFood.Domain.Common;

namespace PinFood.Application.Common.Interfaces.Infrastructure.Services;

public interface IFileStorage
{
	Task<string> SaveFileAsync(IFileData fileData, string folderPath, CancellationToken cancellationToken = default);
	string GetFilePath(string fileName, string folderPath);
}