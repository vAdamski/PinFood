using MediatR;
using PinFood.Domain.Common;

namespace PinFood.Application.Common.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}