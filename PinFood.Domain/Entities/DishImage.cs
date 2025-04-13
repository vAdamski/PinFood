using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Domain.Entities;

public class DishImage : AuditableEntity
{
	public Guid DishId { get; private set; }
	public Dish Dish { get; private set; }

	public string FilePath { get; private set; }
	
	private DishImage() { }
	
	private DishImage(Dish dish, string filePath)
	{
		DishId = dish.Id;
		FilePath = filePath;
	}
	
	public static Result<DishImage> Create(Dish dish, string filePath)
	{
		if (dish == null)
			return Result.Failure<DishImage>(DomainErrors.DishImage.InvalidDish);
		
		if (string.IsNullOrWhiteSpace(filePath))
			return Result.Failure<DishImage>(DomainErrors.DishImage.InvalidFilePath);
		
		return Result.Success(new DishImage(dish, filePath));
	}
}