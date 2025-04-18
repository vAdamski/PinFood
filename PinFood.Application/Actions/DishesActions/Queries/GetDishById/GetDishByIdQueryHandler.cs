using Microsoft.EntityFrameworkCore;
using PinFood.Application.Actions.DishesActions.Commands.CreateDish;
using PinFood.Application.Actions.DishesActions.Shared;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Application.Common.Interfaces.Application.Providers;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishById;

public class GetDishByIdQueryHandler(
	IAppDbContext ctx,
	IFileUrlProvider fileUrlProvider,
	IDishesRepository dishesRepository)
	: IQueryHandler<GetDishByIdQuery, DishViewModel>
{
	public async Task<Result<DishViewModel>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
	{
		var dish = await dishesRepository.GetByIdAsync(request.Id, cancellationToken);

		if (dish is null)
			return Result.Failure<DishViewModel>(DomainErrors.Dish.NotFound);

		var dishViewModel = new DishViewModel
		{
			Name = dish.DishName,
			Description = dish.Description,
			Images = dish.DishImages.Select(i => fileUrlProvider.GenerateFileUrl(i.FilePath)).ToList(),
			RecipeSteps = dish.RecipeSteps.Select(rs => new RecipeStepDto
			{
				Order = rs.Order,
				Name = rs.Name,
				Description = rs.Description
			}).ToList()
		};

		return Result.Success(dishViewModel);
	}
}