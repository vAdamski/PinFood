using MediatR;
using PinFood.Domain.Common;

namespace PinFood.Application.Common.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
	: IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}