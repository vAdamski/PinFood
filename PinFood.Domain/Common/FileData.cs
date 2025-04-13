using Microsoft.AspNetCore.Http;

namespace PinFood.Domain.Common;

public class FileData(IFormFile formFile) : IFileData
{
	public string ContentType => formFile.ContentType;

	public long Length => formFile.Length;

	public string Name => formFile.FileName;

	public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
	{
		await formFile.CopyToAsync(target, cancellationToken);
	}
}