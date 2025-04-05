using MediatR;

namespace PinFood.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;