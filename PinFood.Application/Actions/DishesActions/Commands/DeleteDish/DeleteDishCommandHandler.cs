using Microsoft.EntityFrameworkCore;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Common;
using PinFood.Domain.Enums;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.DishesActions.Commands.DeleteDish;

public class DeleteDishCommandHandler(IDishesRepository dishesRepository, IUnitOfWork unitOfWork)
	: ICommandHandler<DeleteDishCommand>
{
	public async Task<Result> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
	{
		var deletionResult = await dishesRepository.DeleteByIdAsync(request.Id, cancellationToken);

		if (deletionResult.IsFailure)
			return Result.Failure(deletionResult.Error);

		await unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
