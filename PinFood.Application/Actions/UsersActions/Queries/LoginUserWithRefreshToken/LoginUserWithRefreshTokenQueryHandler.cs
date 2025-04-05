using Microsoft.EntityFrameworkCore;
using PinFood.Application.Actions.UsersActions.Queries.Shared;
using PinFood.Application.Common.Abstraction.Messaging;
using PinFood.Application.Common.Interfaces.Application.Providers;
using PinFood.Application.Common.Interfaces.Persistence;
using PinFood.Domain.Common;
using PinFood.Domain.Entities;
using PinFood.Domain.Errors;

namespace PinFood.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

public class LoginUserWithRefreshTokenQueryHandler(IAppDbContext ctx, ITokenProvider tokenProvider)
	: IQueryHandler<LoginUserWithRefreshTokenQuery, LoginUserResponse>
{
	public async Task<Result<LoginUserResponse>> Handle(LoginUserWithRefreshTokenQuery request,
		CancellationToken cancellationToken)
	{
		RefreshToken? refreshToken = await ctx.RefreshTokens
			.Include(r => r.User)
			.FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken);
		
		if (refreshToken is null)
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.InvalidRefreshToken);
		
		if (IsTokenExpired(refreshToken))
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.ExpiredRefreshToken);
		
		string accessToken = tokenProvider.CreateJwtToken(refreshToken.User);
		RefreshToken newRefreshToken = tokenProvider.CreateRefreshToken(refreshToken.User);

		refreshToken.Token = newRefreshToken.Token;
		refreshToken.ExpiresTime = newRefreshToken.ExpiresTime;

		await ctx.SaveChangesAsync(cancellationToken);

		return Result.Success(new LoginUserResponse()
		{
			Token = accessToken,
			RefreshToken = refreshToken.Token,
		});
	}
	
	private bool IsTokenExpired(RefreshToken refreshToken)
	{
		return refreshToken.ExpiresTime < DateTime.UtcNow;
	}
}