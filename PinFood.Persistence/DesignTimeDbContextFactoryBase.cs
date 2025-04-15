using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PinFood.Persistence;

public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
	where TContext : DbContext
{
	private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

	public TContext CreateDbContext(string[] args)
	{
		var basePath = Directory.GetCurrentDirectory() +
		               String.Format("{0}..{0}PinFood.Api", Path.DirectorySeparatorChar);

		var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment) ?? "Development";

		return Create(basePath, environmentName);
	}

	protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

	private TContext Create(string basePath, string environmentName)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(basePath)
			.AddJsonFile("appsettings.json")
			.AddJsonFile($"appsettings.Local.json", optional: true)
			.AddJsonFile($"appsettings.{environmentName}.json", optional: true)
			.AddEnvironmentVariables()
			.Build();

		var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
		                       configuration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrEmpty(connectionString))
			throw new InvalidOperationException("Could not find a connection string named 'DefaultConnection'.");

		return Create(connectionString);
	}

	private TContext Create(string connectionString)
	{
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException($"Connection string '{connectionString}' is null or empty.",
				nameof(connectionString));
		}

		Console.WriteLine($"DesignTimeDbContextFactoryBase.CreateJwtToken(string): Connection string: '{connectionString}'.");

		var optionBuilder = new DbContextOptionsBuilder<TContext>();

		optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		return CreateNewInstance(optionBuilder.Options);
	}
}