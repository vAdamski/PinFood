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
	}
	
	public static class Dish
	{
		public static Error InvalidName =>
			new Error("InvalidName", $"Dish name cannot be empty.");
		
		public static Error InvalidDishNameLenght =>
			new Error("DishNameTooLong", $"Dish name should be between 1 and 256 characters.");
		
		public static Error DescriptionTooLong =>
			new Error("InvalidDescription", $"Dish description cannot be empty.");
		
		public static Error NotFound => 
			new Error("DishNotFound", $"Dish with this id not found.");
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
		public static Error InvalidFilePath =>
			new Error("InvalidFilePath", $"File path cannot be empty.");
	}
	
	public static class FavoriteDish
	{
		public static Error InvalidEmail =>
			new Error("InvalidEmail", $"Email cannot be empty.");
		
		public static Error DishCannotBeNull =>
			new Error("DishCannotBeNull", $"Dish cannot be null.");
		
		public static Error FavoriteDishNotFound =>
			new Error("FavoriteDishNotFound", $"Favorite dish not found.");
	}
}