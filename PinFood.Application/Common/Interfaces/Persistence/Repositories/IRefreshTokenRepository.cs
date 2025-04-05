using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository
{
	Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}