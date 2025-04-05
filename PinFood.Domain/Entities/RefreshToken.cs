namespace PinFood.Domain.Entities;

public class RefreshToken
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public User User { get; set; }
	
	public string Token { get; set; }
	public DateTime ExpiresTime { get; set; }
	
	
	public static RefreshToken Create(Guid userId, string token, DateTime expiresTime)
	{
		return new RefreshToken
		{
			Id = Guid.NewGuid(),
			UserId = userId,
			Token = token,
			ExpiresTime = expiresTime
		};
	}
}