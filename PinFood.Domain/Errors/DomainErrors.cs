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
	
	public static class Dish
	{
		public static Error InvalidName =>
			new Error("InvalidName", $"Dish name cannot be empty.");
		
		public static Error InvalidDishNameLenght =>
			new Error("DishNameTooLong", $"Dish name should be between 1 and 256 characters.");
		
		public static Error DescriptionTooLong =>
			new Error("InvalidDescription", $"Dish description cannot be empty.");
		
		public static Error InvalidRecipeStep =>
			new Error("InvalidRecipeStep", $"Recipe step cannot be empty.");
	}
	
	public static class RecipeStep
	{
		public static Error InvalidDish =>
			new Error("InvalidDish", $"Dish cannot be empty.");
		
		public static Error InvalidName =>
			new Error("InvalidName", $"Recipe step name cannot be empty.");
		
		public static Error InvalidDescription =>
			new Error("InvalidDescription", $"Recipe step description cannot be empty.");
		
		public static Error InvalidOrder =>
			new Error("InvalidOrder", $"Recipe step order cannot be less than 0.");
		
		public static Error OrderAlreadyExists =>
			new Error("OrderAlreadyExists", $"Recipe step with this order already exists.");
	}
	
	public static class DishImage
	{
		public static Error InvalidDish =>
			new Error("InvalidDish", $"Dish cannot be empty.");
		
		public static Error InvalidFileData =>
			new Error("InvalidFileData", $"File data cannot be empty.");
		
		public static Error InvalidFileName =>
			new Error("InvalidFileName", $"File name cannot be empty.");
		
		public static Error InvalidFilePath =>
			new Error("InvalidFilePath", $"File path cannot be empty.");
	}
}