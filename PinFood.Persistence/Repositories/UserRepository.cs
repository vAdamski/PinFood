using Microsoft.EntityFrameworkCore;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Application.Common.Interfaces.Persistence.Repositories;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Repositories;

public class UserRepository(IUnitOfWork unitOfWork, IAppDbContext ctx) : IUserRepository
{
	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await ctx.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		await ctx.Users.AddAsync(user, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}