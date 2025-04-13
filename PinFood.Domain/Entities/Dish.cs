using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Domain.Entities;

public class Dish : AuditableEntity
{
	public string DishName { get; private set; } = "";
	public string Description { get; private set; } = "";
	private List<RecipeStep> _recipeSteps = new();
	public IReadOnlyCollection<RecipeStep> RecipeSteps => _recipeSteps;
	
	private List<DishImage> _dishImages = new();
	public IReadOnlyCollection<DishImage> DishImages => _dishImages;

	private Dish()
	{
	}

	private Dish(string dishName, string description)
	{
		DishName = dishName;
		Description = description;
	}

	public static Result<Dish> Create(string dishName, string description)
	{
		if (string.IsNullOrWhiteSpace(dishName))
			return Result.Failure<Dish>(DomainErrors.Dish.InvalidName);

		if (dishName.Length < 1 || dishName.Length > 256)
			return Result.Failure<Dish>(DomainErrors.Dish.InvalidDishNameLenght);

		if (dishName.Length > 1000)
			return Result.Failure<Dish>(DomainErrors.Dish.DescriptionTooLong);

		return new Dish(dishName, description);
	}

	public Result AddRecipeStep(int order, string name, string description)
	{
		if (_recipeSteps.Any(x => x.Order == order))
			return Result.Failure(DomainErrors.RecipeStep.OrderAlreadyExists);

		var recipeStep = RecipeStep.Create(this, order, name, description);

		if (recipeStep.IsFailure)
			return Result.Failure(recipeStep.Error);

		_recipeSteps.Add(recipeStep.Value);

		return Result.Success();
	}

	public Result AddDishImage(string filePath)
	{
		var dishImage = DishImage.Create(this, filePath);

		if (dishImage.IsFailure)
			return Result.Failure(dishImage.Error);

		_dishImages.Add(dishImage.Value);
		
		return Result.Success();
	}
}