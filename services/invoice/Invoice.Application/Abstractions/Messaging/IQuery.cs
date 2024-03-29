using Invoice.Domain.Abstractions;
using MediatR;

namespace Invoice.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
