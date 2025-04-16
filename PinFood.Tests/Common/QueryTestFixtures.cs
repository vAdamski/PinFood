using PinFood.Persistence;
using PinFood.Tests.Common.MockFactories;

namespace PinFood.Tests.Common;

public class QueryTestFixtures : IDisposable
{
	public AppDbContext Context { get; private set; }

	public QueryTestFixtures()
	{
		Context = AppDbContextMockFactory.Create().Object;
	}
	
	public void Dispose()
	{
		AppDbContextMockFactory.Destroy(Context);
	}
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixtures>
{
	
}