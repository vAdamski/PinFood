using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PinFood.Application.Providers;
using PinFood.Domain.Entities;
using PinFood.Tests.Common.MockFactories;

namespace PinFood.Tests.UnitTests.Application.Providers;

public class TokenProviderTests
{
	private readonly IConfiguration _configuration = ConfigurationMockFactory.Create();
	
	[Fact]
	public void CreateJwtToken_ShouldGenerateValidJwtToken_WhenValidUserProvided()
	{
		// Arrange
		var user = User.Create("Test", "Test", "user@example.com", Guid.NewGuid().ToString());

		var tokenProvider = new TokenProvider(_configuration);

		// Act
		var jwtToken = tokenProvider.CreateJwtToken(user);

		// Assert
		jwtToken.ShouldNotBeNullOrWhiteSpace();

		// Validate the token structure
		var tokenHandler = new JsonWebTokenHandler();
		var validationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = _configuration["Jwt:Issuer"],
			ValidAudience = _configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]))
		};

		var validationResult = tokenHandler.ValidateToken(jwtToken, validationParameters);
		validationResult.IsValid.ShouldBeTrue();
	}

	[Fact]
	public void CreateJwtToken_ShouldIncludeUserClaims()
	{
		// Arrange
		var user = User.Create("Test", "Test", "test@domain.com", Guid.NewGuid().ToString());

		var tokenProvider = new TokenProvider(_configuration);

		// Act
		var jwtToken = tokenProvider.CreateJwtToken(user);

		// Assert
		var tokenHandler = new JsonWebTokenHandler();
		var token = tokenHandler.ReadJsonWebToken(jwtToken);

		token.ShouldNotBeNull();
		token.Subject.ShouldBe(user.Id.ToString());
		token.Claims.ShouldContain(c => c.Type == "email" && c.Value == user.Email);
	}

	[Fact]
	public void CreateRefreshToken_ShouldReturnValidRefreshToken_WhenValidUserProvided()
	{
		// Arrange
		var user = User.Create("Test", "Test", "user@refresh.com", Guid.NewGuid().ToString());

		var tokenProvider = new TokenProvider(_configuration);

		// Act
		var refreshToken = tokenProvider.CreateRefreshToken(user);

		// Assert
		refreshToken.ShouldNotBeNull();
		refreshToken.UserId.ShouldBe(user.Id);
		refreshToken.Token.Length.ShouldBeGreaterThan(0);
		refreshToken.ExpiresTime.ShouldBeGreaterThan(DateTime.UtcNow);
		refreshToken.ExpiresTime.ShouldBeLessThanOrEqualTo(
			DateTime.UtcNow.AddMinutes(1440)); // Configured expiration time
	}

	[Fact]
	public void CreateRefreshToken_ShouldGenerateUniqueTokensForSameUser()
	{
		// Arrange
		var user = User.Create("Test", "Test", "user@refresh.com", Guid.NewGuid().ToString());

		var tokenProvider = new TokenProvider(_configuration);

		// Act
		var refreshToken1 = tokenProvider.CreateRefreshToken(user);
		var refreshToken2 = tokenProvider.CreateRefreshToken(user);

		// Assert
		refreshToken1.ShouldNotBe(refreshToken2); // Ensure tokens are unique
		refreshToken1.Token.ShouldNotBe(refreshToken2.Token); // Ensure token strings are unique
	}
}