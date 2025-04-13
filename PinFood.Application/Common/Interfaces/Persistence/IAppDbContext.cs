using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PinFood.Domain.Entities;

namespace PinFood.Application.Common.Interfaces.Persistence;

public interface IAppDbContext
{
	DatabaseFacade Database { get; }
	DbSet<User> Users { get; set; }
	DbSet<RefreshToken> RefreshTokens { get; set; }
	DbSet<Dish> Dishes { get; set; }
	DbSet<DishImage> DishImages { get; set; }
	DbSet<RecipeStep> RecipeSteps { get; set; }
	
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
	void Dispose();
}