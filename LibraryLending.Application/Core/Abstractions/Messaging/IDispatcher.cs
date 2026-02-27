using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Messaging;

public interface IDispatcher
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default) where TResponse : Result;
    Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken ct = default) where TResponse : Result;
}
