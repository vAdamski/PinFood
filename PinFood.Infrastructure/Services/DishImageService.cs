using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Domain.Common;

namespace PinFood.Infrastructure.Services;

public class DishImageService(IFileStorage fileStorage) : IDishImageService
{
	private readonly string _folderPath = "Images/DishImages";

	/// <summary>
	/// Saves the provided image file to a designated storage location.
	/// </summary>
	/// <param name="fileData">The file data containing the image to be saved.</param>
	/// <returns>Returns the path where the file was saved.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the provided file data is null.</exception>
	public async Task<string> SaveImage(IFileData fileData)
	{
		if (fileData == null) throw new ArgumentNullException(nameof(fileData));

		return await fileStorage.SaveFileAsync(fileData, _folderPath);
	}
}