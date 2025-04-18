using PinFood.Application.Actions.DishesActions.Shared;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Application.Providers;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;

namespace PinFood.Application.Actions.DishesActions.Queries.GetDishes;

public class GetDishesQueryHandler(IDishesRepository dishedRepository, IFileUrlProvider fileUrlProvider)
	: IQueryHandler<GetDishesQuery, DishesViewModel>
{
	public async Task<Result<DishesViewModel>> Handle(GetDishesQuery request, CancellationToken cancellationToken)
	{
		var dishes = await dishedRepository.GetAllAsync(cancellationToken);

		var dishesViewModel = new DishesViewModel
		{
			Dishes = dishes.Select(d => new DishListItemDto()
			{
				Id = d.Id,
				Name = d.DishName,
				Description = d.Description,
				Images = d.DishImages.Select(i => fileUrlProvider.GenerateFileUrl(i.FilePath)).ToList(),
				RecipeSteps = d.RecipeSteps.Select(rs => new RecipeStepDto
				{
					Order = rs.Order,
					Name = rs.Name,
					Description = rs.Description
				}).ToList()
			}).ToList()
		};

		return Result.Success(dishesViewModel);
	}
}