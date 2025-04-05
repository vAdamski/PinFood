using MediatR;
using PinFood.Domain.Common;

namespace PinFood.Application.Common.Abstraction.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}