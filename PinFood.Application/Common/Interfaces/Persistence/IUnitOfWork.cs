namespace PinFood.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	Task BeginTransactionAsync();
	Task CommitTransactionAsync();
	Task RollbackTransactionAsync();
}