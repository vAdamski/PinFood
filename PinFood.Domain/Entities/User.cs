namespace PinFood.Domain.Entities;

public class User
{
	public Guid Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }

	private User()
	{
		
	}
	
	public static User Create(string firstName, string lastName, string email, string passwordHash)
	{
		return new User
		{
			Id = Guid.NewGuid(),
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			PasswordHash = passwordHash
		};
	}
}