using Microsoft.AspNetCore.Http;

namespace PinFood.Domain.Common;

public class ImageFileData(IFormFile formFile) : IFileData
{
	public string ContentType => formFile.ContentType;

	public long Length => formFile.Length;

	public string Name => Guid.NewGuid() + Path.GetExtension(formFile.FileName);

	public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
	{
		await formFile.CopyToAsync(target, cancellationToken);
	}
}