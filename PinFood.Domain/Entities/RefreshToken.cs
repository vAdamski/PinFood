using System.Security.Cryptography;

namespace PinFood.Domain.Entities;

public class RefreshToken
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public User User { get; set; }
	public string Token { get; set; }
	public DateTime ExpiresTime { get; set; }
	
	
	public static RefreshToken Create(Guid userId, DateTime expiresTime)
	{
		return new RefreshToken
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			Token = GenerateRefreshToken(),
			ExpiresTime = expiresTime
		};
	}

	private static string GenerateRefreshToken()
	{
		return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
	}
}