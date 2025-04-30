using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PinFood.Application.Common.Interfaces.Api.Services;
using PinFood.Application.Common.Interfaces.Infrastructure.Services;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;
using PinFood.Domain.Enums;
using PinFood.Domain.Primitives;

namespace PinFood.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	private readonly IDateTime _dateTime;
	private readonly ICurrentUserService _userService;
	private readonly IPublisher _publisher;

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		
	}
	
	public AppDbContext(DbContextOptions<AppDbContext> options, IDateTime dateTime,
		ICurrentUserService userService, IPublisher publisher) : base(options)
	{
		_dateTime = dateTime;
		_userService = userService;
		_publisher = publisher;
	}
	
	
	public DatabaseFacade Database => base.Database;
	
	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
	
	public DbSet<Dish> Dishes { get; set; }
	public DbSet<DishImage> DishImages { get; set; }
	public DbSet<RecipeStep> RecipeSteps { get; set; }
	public DbSet<FavoriteDish> FavoriteDishes { get; set; }
	
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		
		modelBuilder.Ignore<EntityEvent>();
		modelBuilder.Ignore<DomainEvent>();
	}
	
	public void Dispose()
	{
		base.Dispose();
	}
	
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		HandleAuditableEntities();

		var result = base.SaveChangesAsync(cancellationToken);
		
		ExecuteDomainEvents();
		
		return result;
	}

	private void HandleAuditableEntities()
	{
		foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
		{
			switch (entry.State)
			{
				case EntityState.Deleted:
					entry.Entity.ModifiedBy = _userService.Email;
					entry.Entity.Modified = _dateTime.Now;
					entry.Entity.Inactivated = _dateTime.Now;
					entry.Entity.InactivatedBy = _userService.Email;
					entry.Entity.Status = AuditableEntityStatus.Inactive;
					entry.State = EntityState.Modified;
					break;
				case EntityState.Modified:
					entry.Entity.ModifiedBy = _userService.Email;
					entry.Entity.Modified = _dateTime.Now;
					break;
				case EntityState.Added:
					entry.Entity.CreatedBy = _userService.Email;
					entry.Entity.Created = _dateTime.Now;
					entry.Entity.Status = AuditableEntityStatus.Active;
					break;
				default:
					break;
			}
		}

		foreach (var entry in ChangeTracker.Entries<ValueObject>())
		{
			switch (entry.State)
			{
				case EntityState.Deleted:
					entry.State = EntityState.Modified;
					break;
				default:
					break;
			}
		}
	}

	private void ExecuteDomainEvents()
	{
		var domainEvents = ChangeTracker.Entries<AuditableEntity>()
			.Select(x => x.Entity)
			.Where(x => x.DomainEvents.Any())
			.SelectMany(x => x.DomainEvents)
			.ToList();
		
		foreach (var domainEvent in domainEvents)
		{
			_publisher.Publish(domainEvent);
		}
	}
}