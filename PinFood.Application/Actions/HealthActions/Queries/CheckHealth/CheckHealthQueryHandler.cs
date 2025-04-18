using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Domain.Common;

namespace PinFood.Application.Actions.HealthActions.Queries.CheckHealth;

public class CheckHealthQueryHandler(IAppDbContext ctx) : IQueryHandler<CheckHealthQuery, HealthViewModel>
{
	public async Task<Result<HealthViewModel>> Handle(CheckHealthQuery request, CancellationToken cancellationToken)
	{
		var dbConnection = await ctx.Database.CanConnectAsync(cancellationToken);
		
		var result = new HealthViewModel
		{
			ApiStatus = true,
			DatabaseStatus = dbConnection
		};
		
		return Result.Success(result);
	}
}