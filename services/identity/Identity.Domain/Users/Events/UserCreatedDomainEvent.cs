using Identity.Domain.Abstractions;

namespace Identity.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
