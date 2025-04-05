using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task AddAsync(User user, CancellationToken cancellationToken = default);
}