using PinFood.Domain.Common;

namespace PinFood.Domain.Errors;

public static class DomainErrors
{
	public static class User
	{
		public static Error EmailAlreadyExists(string email) =>
			new Error("EmailAlreadyExists", $"User with email {email} already exists.");
		
		public static Error UserNotFound =>
			new Error("UserNotFound", $"User with this email not found.");
		
		public static Error InvalidPassword =>
			new Error("InvalidPassword", $"Invalid password.");
	}
	
	public static class RefreshToken
	{
		public static Error InvalidRefreshToken =>
			new Error("InvalidRefreshToken", $"Invalid refresh token.");
		
		public static Error ExpiredRefreshToken =>
			new Error("ExpiredRefreshToken", $"Expired refresh token.");
		
		public static Error InvalidUser =>
			new Error("InvalidUser", $"Invalid user.");
	}
}