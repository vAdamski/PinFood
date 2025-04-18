using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Application.Actions.DishesActions.Shared;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;
using PinFood.Domain.Enums;
using PinFood.Domain.Errors;

namespace PinFood.Persistence.Repositories;

public class DishesRepository(IAppDbContext ctx) : IDishesRepository
{
	public async Task<Dish?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await ctx.Dishes
			.Include(x => x.DishImages)
			.Include(x => x.RecipeSteps)
			.FirstOrDefaultAsync(x => x.Id == id &&
			                          x.Status == AuditableEntityStatus.Active,
				cancellationToken);
	}
	
	public async Task AddAsync(Dish dish, CancellationToken cancellationToken)
	{
		await ctx.Dishes.AddAsync(dish, cancellationToken);
	}

	public Result AddRecipeSteps(Dish dish, IEnumerable<RecipeStepDto> recipeSteps)
	{
		foreach (var recipeStep in recipeSteps)
		{
			var result = dish.AddRecipeStep(recipeStep.Order, recipeStep.Name, recipeStep.Description);
			if (result.IsFailure)
				return result;
		}

		return Result.Success();
	}

	public async Task<Result> AddDishImagesAsync(Dish dish, IEnumerable<IFormFile> images,
		IDishImageService dishImageService)
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

	public async Task<Result> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var dish = await GetByIdAsync(id, cancellationToken);
		if (dish is null)
			return Result.Failure(DomainErrors.Dish.NotFound);

		ctx.Dishes.Remove(dish);

		return Result.Success();
	}

	public async Task<List<Dish>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await ctx.Dishes
			.Include(x => x.DishImages)
			.Include(x => x.RecipeSteps)
			.Where(x => x.Status == AuditableEntityStatus.Active)
			.ToListAsync(cancellationToken);
	}
}