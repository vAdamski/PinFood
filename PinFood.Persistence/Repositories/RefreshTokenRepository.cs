using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Repositories;

public class RefreshTokenRepository(IAppDbContext ctx, IUnitOfWork unitOfWork) : IRefreshTokenRepository
{
	public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
	{
		await ctx.RefreshTokens.AddAsync(refreshToken, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}