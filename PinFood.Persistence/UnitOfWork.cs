using Microsoft.EntityFrameworkCore.Storage;
using PinFood.Application.Common.Interfaces.Persistence;

namespace PinFood.Persistence;

public class UnitOfWork : IUnitOfWork
{
	private readonly IAppDbContext _context;
	private IDbContextTransaction _currentTransaction;

	public UnitOfWork(IAppDbContext context)
	{
		_context = context;
	}

	public async Task BeginTransactionAsync()
	{
		if (_currentTransaction != null)
		{
			return;
		}
        
		_currentTransaction = await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitTransactionAsync()
	{
		try
		{
			await _context.SaveChangesAsync();
			await _currentTransaction?.CommitAsync();
		}
		catch
		{
			await RollbackTransactionAsync();
			throw;
		}
		finally
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.DisposeAsync();
				_currentTransaction = null;
			}
		}
	}

	public async Task RollbackTransactionAsync()
	{
		try
		{
			await _currentTransaction?.RollbackAsync();
		}
		finally
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.DisposeAsync();
				_currentTransaction = null;
			}
		}
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}