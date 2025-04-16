using PinFood.Persistence;

namespace PinFood.Tests.Common.Seeds;

public static class DatabaseSeed
{
	public static async Task Seed(this AppDbContext context)
	{
		
		
		await context.SaveChangesAsync();
	}
}