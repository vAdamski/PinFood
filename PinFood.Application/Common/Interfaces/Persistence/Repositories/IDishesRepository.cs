using Microsoft.AspNetCore.Http;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Application.Actions.DishesActions.Shared;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Persistence.Repositories;

public interface IDishesRepository
{
	Task AddAsync(Dish dish, CancellationToken cancellationToken);
	Result AddRecipeSteps(Dish dish, IEnumerable<RecipeStepDto> recipeSteps);
	Task<Result> AddDishImagesAsync(Dish dish, IEnumerable<IFormFile> images, IDishImageService dishImageService);
	Task<Dish?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Result> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<List<Dish>> GetAllAsync(CancellationToken cancellationToken);
}