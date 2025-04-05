using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PinFood.Application.Common.Interfaces.Application.Providers;
using PinFood.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PinFood.Application.Providers;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
	public string CreateJwtToken(User user)
	{
		string secretKey = configuration["Jwt:Secret"]!;
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
			}),
			Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
			SigningCredentials = credentials,
			Issuer = configuration["Jwt:Issuer"],
			Audience = configuration["Jwt:Audience"],
		};
		
		var tokenHandler = new JsonWebTokenHandler();
		
		var token = tokenHandler.CreateToken(tokenDescriptor);
		
		return token;
	}
	
	public RefreshToken CreateRefreshToken(User user)
	{
		var expirationTimeInMinutes = configuration.GetValue<int>("Jwt:RefreshTokenExpirationInMinutes");
		var expirationTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);
		
		return RefreshToken.Create(user.Id, expirationTime);
	}
}