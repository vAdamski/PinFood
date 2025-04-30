using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Domain.Entities;

public class FavoriteDish : AuditableEntity
{
	public Guid DishId { get; set; }
	public Dish? Dish { get; set; }
	public string Email { get; set; }

	private FavoriteDish()
	{
		
	}

	private FavoriteDish(Dish dish, string email)
	{
		DishId = dish.Id;
		Dish = dish;
		Email = email;
	}
	
	public static Result<FavoriteDish> AddToFavorites(Dish dish, string email)
	{
		if (string.IsNullOrWhiteSpace(email))
			return Result.Failure<FavoriteDish>(DomainErrors.FavoriteDish.InvalidEmail);

		if (dish == null)
			return Result.Failure<FavoriteDish>(DomainErrors.FavoriteDish.DishCannotBeNull);

		var favoriteDish = new FavoriteDish(dish, email);

		return Result.Success(favoriteDish);
	}
}