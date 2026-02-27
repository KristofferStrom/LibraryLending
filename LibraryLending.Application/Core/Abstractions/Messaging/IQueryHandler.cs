using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Messaging;


public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : Result
{
    Task<TResponse> Handle(TQuery query, CancellationToken ct);
}
