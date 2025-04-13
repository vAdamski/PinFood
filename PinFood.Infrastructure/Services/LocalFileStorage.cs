using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Domain.Common;

namespace PinFood.Infrastructure.Services;

public class LocalFileStorage(string basePath) : IFileStorage
{
	private readonly string _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));

	/// <summary>
	/// Saves a file asynchronously to the designated folder path within the local file storage.
	/// </summary>
	/// <param name="fileData">An object representing the file data to be saved, including its name and content.</param>
	/// <param name="folderPath">The relative path of the folder where the file should be stored.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the operation to complete.</param>
	/// <returns>A string representing the full path of the saved file in the storage system.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the fileData argument is null.</exception>
	public async Task<string> SaveFileAsync(IFileData fileData, string folderPath, CancellationToken cancellationToken = default)
	{
		if (fileData == null) throw new ArgumentNullException(nameof(fileData));

		var directoryPath = Path.Combine(_basePath, folderPath);
		Directory.CreateDirectory(directoryPath);

		var filePath = Path.Combine(directoryPath, fileData.Name);
		await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
		await fileData.CopyToAsync(fileStream, cancellationToken);
		
		return Path.Combine(folderPath, fileData.Name);
	}

	public string GetFilePath(string fileName, string folderPath)
	{
		if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
		if (string.IsNullOrWhiteSpace(folderPath)) throw new ArgumentNullException(nameof(folderPath));

		return Path.Combine(folderPath, fileName);
	}
}