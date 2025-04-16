using Microsoft.Extensions.Configuration;
using Moq;

namespace PinFood.Tests.Common.MockFactories;

public static class ConfigurationMockFactory
{
	public static IConfiguration Create(Dictionary<string, string> settings = null)
	{
		var inMemorySettings = new Dictionary<string, string>
		{
			{ "Jwt:Secret", Guid.NewGuid().ToString() },
			{ "Jwt:Issuer", "https://testissuer.com" },
			{ "Jwt:Audience", "https://testaudience.com" },
			{ "Jwt:ExpirationInMinutes", "60" },
			{ "Jwt:RefreshTokenExpirationInMinutes", "1440" },
		};

		if (settings != null)
		{
			foreach (var setting in settings)
			{
				inMemorySettings[setting.Key] = setting.Value;
			}
		}

		return new ConfigurationBuilder()
			.AddInMemoryCollection(inMemorySettings)
			.Build();
	}
}