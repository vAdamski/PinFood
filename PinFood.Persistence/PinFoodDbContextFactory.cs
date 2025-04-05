using Microsoft.EntityFrameworkCore;

namespace PinFood.Persistence;

public class PinFoodDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
	protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
	{
		return new AppDbContext(options);
	}
}