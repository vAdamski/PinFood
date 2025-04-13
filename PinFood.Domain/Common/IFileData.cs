namespace PinFood.Domain.Common;

public interface IFileData
{
	string ContentType { get; }
	long Length { get; }
	string Name { get; }
	Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
}