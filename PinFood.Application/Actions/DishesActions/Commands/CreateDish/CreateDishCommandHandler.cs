using Microsoft.AspNetCore.Http;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;

namespace PinFood.Application.Actions.DishesActions.Commands.CreateDish;

public class CreateDishCommandHandler(
	IDishImageService dishImageService,
	IUnitOfWork unitOfWork,
	IDishesRepository dishesRepository)
	: ICommandHandler<CreateDishCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		var dish = Dish.Create(request.Name, request.Description).Value;
		
		var recipeStepsResult = dishesRepository.AddRecipeSteps(dish, request.RecipeSteps);
		if (recipeStepsResult.IsFailure)
			return Result.Failure<Guid>(recipeStepsResult.Error);
		
		var imagesResult = await dishesRepository.AddDishImagesAsync(dish, request.Images, dishImageService);
		if (imagesResult.IsFailure)
			return Result.Failure<Guid>(imagesResult.Error);

		await dishesRepository.AddAsync(dish, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success(dish.Id);
	}
}