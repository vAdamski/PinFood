using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Domain.Entities;

public class RecipeStep : AuditableEntity
{
	public int Order { get; private set; }
	public string Name { get; private set; } = "";
	public string Description { get; private set; } = "";
	
	public Guid DishId { get; private set; }
	public Dish Dish { get; private set; }

	private RecipeStep()
	{
		
	}
	
	private RecipeStep(Dish dish, int order, string name, string description)
	{
		DishId = dish.Id;
		Dish = dish;
		Order = order;
		Name = name;
		Description = description;
	}

	public static Result<RecipeStep> Create(Dish dish, int order, string name, string description)
	{
		if (dish == null)
			return Result.Failure<RecipeStep>(DomainErrors.RecipeStep.InvalidDish);
		
		if (string.IsNullOrWhiteSpace(name))
			return Result.Failure<RecipeStep>(DomainErrors.RecipeStep.InvalidName);
		
		if (string.IsNullOrWhiteSpace(description))
			return Result.Failure<RecipeStep>(DomainErrors.RecipeStep.InvalidDescription);
		
		if (order < 0)
			return Result.Failure<RecipeStep>(DomainErrors.RecipeStep.InvalidOrder);
		
		return new RecipeStep(dish, order, name, description);
	}
}