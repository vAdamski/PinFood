namespace PinFood.Application.Common.Interfaces.Application.Services;

public interface IPasswordHasher
{
	string Hash(string password);
	bool Verify(string password, string passwordHash);
}