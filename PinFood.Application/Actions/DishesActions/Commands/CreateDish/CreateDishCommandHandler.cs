using Microsoft.AspNetCore.Http;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;

namespace PinFood.Application.Actions.DishesActions.Commands.CreateDish;

public class CreateDishCommandHandler(IAppDbContext ctx, IDishImageService dishImageService, IUnitOfWork unitOfWork)
	: ICommandHandler<CreateDishCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		var dish = Dish.Create(request.Name, request.Description).Value;

		var recipeStepsResult = AddRecipeSteps(dish, request.RecipeSteps);
		if (recipeStepsResult.IsFailure)
			return Result.Failure<Guid>(recipeStepsResult.Error);

		var imagesResult = await AddDishImagesAsync(dish, request.Images);
		if (imagesResult.IsFailure)
			return Result.Failure<Guid>(imagesResult.Error);

		await ctx.Dishes.AddAsync(dish, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success(dish.Id);
	}

	private Result AddRecipeSteps(Dish dish, IEnumerable<RecipeStepDto> recipeSteps)
	{
		foreach (var recipeStep in recipeSteps)
		{
			var result = dish.AddRecipeStep(recipeStep.Order, recipeStep.Name, recipeStep.Description);
			if (result.IsFailure)
				return result;
		}

		return Result.Success();
	}

	private async Task<Result> AddDishImagesAsync(Dish dish, IEnumerable<IFormFile> images)
	{
		foreach (var image in images)
		{
			var fileData = new ImageFileData(image);
			var filePath = await dishImageService.SaveImage(fileData);

			var result = dish.AddDishImage(filePath);
			if (result.IsFailure)
				return result;
		}

		return Result.Success();
	}
}